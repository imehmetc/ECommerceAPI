using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        

        [HttpPost("create-order")]
        public async Task<IActionResult> AddOrder([FromBody] OrderDto orderDto)
        {
            var result = await _orderService.CreateOrderAsync(orderDto);
            if (result)
            {
                return Ok(new { success = true, message = "Order added successfully." });
            }
            else
            {
                return BadRequest(new { success = false, message = "Failed to add order." });
            }
        }
    }
}
