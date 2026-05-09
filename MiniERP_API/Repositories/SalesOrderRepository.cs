using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MiniERP_API.Models.Entities;
using MiniERP_API.Helpers;
using MiniERP_API.Repositories.Interfaces;

namespace MiniERP_API.Repositories
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly string _connectionString;

        public SalesOrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int CreateOrder(SalesOrder order)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        var cmdHeader = new SqlCommand(Queries.InsertSalesOrder, conn, transaction);
                        cmdHeader.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                        cmdHeader.Parameters.AddWithValue("@CustomerId", (object)order.CustomerId ?? DBNull.Value);
                        cmdHeader.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                        cmdHeader.Parameters.AddWithValue("@Status", order.Status);
                        cmdHeader.Parameters.AddWithValue("@PaymentMethod", order.PaymentMethod);
                        cmdHeader.Parameters.AddWithValue("@PaymentStatus", order.PaymentStatus);
                        cmdHeader.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                        cmdHeader.Parameters.AddWithValue("@ShippingAddress", (object)order.ShippingAddress ?? DBNull.Value);
                        cmdHeader.Parameters.AddWithValue("@Notes", (object)order.Notes ?? DBNull.Value);
                        cmdHeader.Parameters.AddWithValue("@CreatedBy", (object)order.CreatedBy ?? DBNull.Value);

                        int orderId = (int)cmdHeader.ExecuteScalar();

                        foreach (var item in order.Items)
                        {
                            var cmdItem = new SqlCommand(Queries.InsertSalesOrderItem, conn, transaction);
                            cmdItem.Parameters.AddWithValue("@SalesOrderId", orderId);
                            cmdItem.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdItem.Parameters.AddWithValue("@Quantity", item.Quantity);
                            cmdItem.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                            cmdItem.Parameters.AddWithValue("@Discount", item.Discount);
                            cmdItem.ExecuteNonQuery();

                            var cmdStock = new SqlCommand(Queries.UpdateProductStock, conn, transaction);
                            cmdStock.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdStock.Parameters.AddWithValue("@Quantity", item.Quantity);
                            
                            int affected = cmdStock.ExecuteNonQuery();
                            if (affected == 0) throw new Exception($"Sản phẩm ID {item.ProductId} không đủ tồn kho!");
                        }

                        transaction.Commit();
                        return orderId;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public SalesOrder GetById(int id) => null;
    }
}
