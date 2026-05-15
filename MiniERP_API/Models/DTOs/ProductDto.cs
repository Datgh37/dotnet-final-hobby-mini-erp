using System.ComponentModel.DataAnnotations;

namespace MiniERP_API.Models.DTOs
{
    public class ProductDto
    {
        /// <example>1</example>
        public int Id { get; set; }
        
        /// <example>1</example>
        public int? CategoryId { get; set; }
        
        /// <example>2</example>
        public int? BrandId { get; set; }
        
        /// <example>AP-IP15PM</example>
        public string SKU { get; set; }
        
        /// <example>iPhone 15 Pro Max 256GB</example>
        public string Name { get; set; }
        
        /// <example>Apple flagship smartphone with Titanium frame.</example>
        public string Description { get; set; }
        
        /// <example>Cái</example>
        public string Unit { get; set; }
        
        /// <example>1100.00</example>
        public decimal CostPrice { get; set; }
        
        /// <example>1400.00</example>
        public decimal RetailPrice { get; set; }
        
        /// <example>15</example>
        public int StockQuantity { get; set; }
        
        /// <example>https://example.com/iphone15.jpg</example>
        public string ImageUrl { get; set; }
    }

    public class ProductCreateUpdateDto
    {
        /// <example>1</example>
        public int? CategoryId { get; set; }

        /// <example>2</example>
        public int? BrandId { get; set; }
        
        /// <example>AP-IP15PM</example>
        [Required(ErrorMessage = "Mã SKU là bắt buộc.")]
        public string SKU { get; set; }
        
        /// <example>iPhone 15 Pro Max 256GB</example>
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
        public string Name { get; set; }
        
        /// <example>Apple flagship smartphone with Titanium frame.</example>
        public string Description { get; set; }

        /// <example>Cái</example>
        public string Unit { get; set; }
        
        /// <example>1100.00</example>
        [Range(0, double.MaxValue, ErrorMessage = "Giá vốn không được âm.")]
        public decimal CostPrice { get; set; }
        
        /// <example>1400.00</example>
        [Range(0, double.MaxValue, ErrorMessage = "Giá bán không được âm.")]
        public decimal RetailPrice { get; set; }
        
        /// <example>15</example>
        public int StockQuantity { get; set; }

        /// <example>https://example.com/iphone15.jpg</example>
        public string ImageUrl { get; set; }
    }
}
