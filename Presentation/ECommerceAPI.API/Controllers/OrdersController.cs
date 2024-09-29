using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Persistence.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetPastOrdersByCustomerId(string customerId)
        {
            var orders = await _orderService.GetPastOrdersByCustomerIdAsync(customerId);
            return Ok(orders);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetailsById(Guid orderId)
        {
            var order = await _orderService.GetOrderDetailsByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("{orderId}/status")]
        public async Task<IActionResult> GetOrderStatusById(Guid orderId)
        {
            try
            {
                var orderStatus = await _orderService.GetOrderStatusByIdAsync(orderId);
                return Ok(orderStatus);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
