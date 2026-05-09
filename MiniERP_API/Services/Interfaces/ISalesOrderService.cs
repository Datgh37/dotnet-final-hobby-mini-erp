using MiniERP_API.Models.DTOs;

namespace MiniERP_API.Services.Interfaces
{
    public interface ISalesOrderService
    {
        int PlaceOrder(CreateSalesOrderDto dto);
    }
}
