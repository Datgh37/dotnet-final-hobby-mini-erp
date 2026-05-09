using System.Collections.Generic;
using AutoMapper;
using MiniERP_API.Models.DTOs;
using MiniERP_API.Models.Entities;
using MiniERP_API.Repositories.Interfaces;
using MiniERP_API.Services.Interfaces;

namespace MiniERP_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public IEnumerable<ProductDto> GetActiveProducts()
        {
            var products = _repo.GetAll();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public ProductDto GetProduct(int id)
        {
            var product = _repo.GetById(id);
            return _mapper.Map<ProductDto>(product);
        }

        public int CreateProduct(Product product) => _repo.Add(product);
        public void UpdateProduct(Product product) => _repo.Update(product);
        public void DeleteProduct(int id) => _repo.Delete(id);
    }
}
