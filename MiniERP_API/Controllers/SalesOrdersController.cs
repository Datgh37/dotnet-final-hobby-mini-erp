using Microsoft.AspNetCore.Mvc;
using MiniERP_API.Models.DTOs;
using MiniERP_API.Services.Interfaces;

namespace MiniERP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesOrdersController : ControllerBase
    {
        private readonly ISalesOrderService _service;
        public SalesOrdersController(ISalesOrderService service) => _service = service;

        [HttpPost]
        public IActionResult CreateNewOrder(CreateSalesOrderDto dto)
        {
            try
            {
                var id = _service.PlaceOrder(dto);
                return Ok(new { Message = "Đơn hàng đã được tạo thành công", OrderId = id });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
