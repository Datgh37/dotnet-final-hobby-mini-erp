namespace MiniERP_API.Models.DTOs
{
    public class CustomerDto
    {
        /// <example>1</example>
        public int Id { get; set; }
        
        /// <example>Le Van C</example>
        public string Name { get; set; }
        
        /// <example>customerC@outlook.com</example>
        public string Email { get; set; }
        
        /// <example>0912233445</example>
        public string Phone { get; set; }
        
        /// <example>789 Road, District 7, Ho Chi Minh</example>
        public string Address { get; set; }
    }

    public class CustomerCreateUpdateDto
    {
        /// <example>Le Van C</example>
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Tên khách hàng là bắt buộc.")]
        public string Name { get; set; }
        
        /// <example>customerC@outlook.com</example>
        [System.ComponentModel.DataAnnotations.EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; }
        
        /// <example>0912233445</example>
        public string Phone { get; set; }

        /// <example>789 Road, District 7, Ho Chi Minh</example>
        public string Address { get; set; }
    }
}
