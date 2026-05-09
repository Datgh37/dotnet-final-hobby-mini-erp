using System;
using System.Linq;
using AutoMapper;
using MiniERP_API.Models.DTOs;
using MiniERP_API.Models.Entities;
using MiniERP_API.Repositories.Interfaces;
using MiniERP_API.Services.Interfaces;

namespace MiniERP_API.Services
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly ISalesOrderRepository _orderRepo;
        private readonly IMapper _mapper;

        public SalesOrderService(ISalesOrderRepository orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        public int PlaceOrder(CreateSalesOrderDto dto)
        {
            // Map từ DTO sang Entity
            var order = _mapper.Map<SalesOrder>(dto);
            
            // Bổ sung logic nghiệp vụ (Số hóa số đơn hàng và tính tổng tiền)
            order.OrderNumber = "SO-" + DateTime.Now.Ticks.ToString().Substring(10);
            order.TotalAmount = dto.Items.Sum(i => (i.UnitPrice - i.Discount) * i.Quantity);

            return _orderRepo.CreateOrder(order);
        }
    }
}
