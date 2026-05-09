using System.Collections.Generic;

namespace MiniERP_API.Models.DTOs
{
    public class CreateSalesOrderDto
    {
        public int? CustomerId { get; set; }
        public string PaymentMethod { get; set; }
        public string ShippingAddress { get; set; }
        public string Notes { get; set; }
        public List<CreateSalesOrderItemDto> Items { get; set; }
    }
}
