using System.Collections.Generic;
using MiniERP_API.Models.DTOs;
using MiniERP_API.Models.Entities;

namespace MiniERP_API.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetActiveProducts();
        ProductDto GetProduct(int id);
        int CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
