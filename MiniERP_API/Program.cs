using MiniERP_API.Helpers;
using MiniERP_API.Repositories;
using MiniERP_API.Repositories.Interfaces;
using MiniERP_API.Services;
using MiniERP_API.Services.Interfaces;

namespace MiniERP_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Đăng ký AutoMapper
            builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(AutoMapperProfile).Assembly));

            // Cấu hình NSwag thay thế cho AddOpenApi mặc định của .NET
            builder.Services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Title = "Hobby Store Mini ERP API";
                    document.Info.Version = "v1";
                    document.Info.Description = "Hệ thống quản lý Mini ERP cho Hobby Store";
                };
            });

            // DI Registrations
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<ISalesOrderRepository, SalesOrderRepository>();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ISalesOrderService, SalesOrderService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Sử dụng Middleware của NSwag
                app.UseOpenApi();
                app.UseSwaggerUi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
