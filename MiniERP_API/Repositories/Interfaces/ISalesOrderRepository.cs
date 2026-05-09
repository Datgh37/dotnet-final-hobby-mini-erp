using MiniERP_API.Models.Entities;

namespace MiniERP_API.Repositories.Interfaces
{
    public interface ISalesOrderRepository
    {
        int CreateOrder(SalesOrder order);
        SalesOrder GetById(int id);
    }
}
