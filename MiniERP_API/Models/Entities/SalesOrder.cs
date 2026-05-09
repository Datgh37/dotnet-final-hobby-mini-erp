using System;
using System.Collections.Generic;

namespace MiniERP_API.Models.Entities
{
    public class SalesOrder : BaseEntity
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int? CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public string Notes { get; set; }
        public int? CreatedBy { get; set; }
        public List<SalesOrderItem> Items { get; set; } = new List<SalesOrderItem>();
    }
}
