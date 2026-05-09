using AutoMapper;
using MiniERP_API.Models.Entities;
using MiniERP_API.Models.DTOs;

namespace MiniERP_API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Product Mappings
            CreateMap<Product, ProductDto>();

            // SalesOrder Mappings
            CreateMap<CreateSalesOrderDto, SalesOrder>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "NEW"))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => "PENDING"));

            CreateMap<CreateSalesOrderItemDto, SalesOrderItem>();
            
            CreateMap<Supplier, SupplierDto>();
        }
    }
}
