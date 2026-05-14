namespace MiniERP_API.Models.DTOs
{
    public class BrandDto
    {
        /// <example>1</example>
        public int Id { get; set; }
        
        /// <example>Apple</example>
        public string Name { get; set; }
        
        /// <example>Technology and Innovation</example>
        public string Description { get; set; }
    }

    public class BrandCreateUpdateDto
    {
        /// <example>Apple</example>
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Tên nhãn hàng là bắt buộc.")]
        public string Name { get; set; }

        /// <example>Technology and Innovation</example>
        public string Description { get; set; }
    }
}
