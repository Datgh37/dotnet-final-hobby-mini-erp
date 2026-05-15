using System.ComponentModel.DataAnnotations;

namespace MiniERP_API.Models.DTOs
{
    public class CreateSalesOrderItemDto
    {
        /// <example>1</example>
        [Required(ErrorMessage = "Sản phẩm là bắt buộc.")]
        public int ProductId { get; set; }
        
        /// <example>2</example>
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải ít nhất là 1.")]
        public int Quantity { get; set; }
        
        /// <example>150000</example>
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá không được âm.")]
        public decimal UnitPrice { get; set; }
        
        /// <example>10000</example>
        [Range(0, double.MaxValue, ErrorMessage = "Giảm giá không được âm.")]
        public decimal Discount { get; set; }
    }
}
