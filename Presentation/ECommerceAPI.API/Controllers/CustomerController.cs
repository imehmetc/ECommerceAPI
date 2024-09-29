using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Persistence.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Authorize(Roles = "Member")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IAddressService _addressService;


        public CustomerController(ICustomerService customerService, IAddressService addressService, IOrderService orderService)
        {
            _customerService = customerService;
            _addressService = addressService;
            _orderService = orderService;
        }
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userPrincipal = User;
            var customerDto = await _customerService.GetProfileAsync(userPrincipal);
            if (customerDto == null)
            {
                return NotFound("Customer not found.");
            }
            return Ok(customerDto);
        }

        [HttpPost("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] CustomerDto customerDto, [FromForm] IFormFile? photoUrl)
        {
            if (customerDto == null)
            {
                return BadRequest("Invalid data.");
            }
            if (photoUrl != null)
            {
                var directoryPath = Path.Combine("wwwroot/uploads/profile-photos");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Fotoğrafı sunucuda kaydet
                var filePath = Path.Combine(directoryPath, photoUrl.FileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photoUrl.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }

                customerDto.Photo = photoUrl.FileName;
            }
            var userPrincipal = User;
            var result = await _customerService.UpdateProfileAsync(customerDto, userPrincipal);

            if (result.Succeeded)
            {
                return Ok("Profile updated successfully.");
            }
            return BadRequest("Failed to update profile.");
        }
        [HttpGet("get-addresses")]
        public async Task<IActionResult> GetAddresses()
        {
            var userPrincipal = User;
            var addresses = await _customerService.GetCustomerAddresses(userPrincipal);
            if (addresses == null)
            {
                return NotFound("Customer's Addresses not found.");
            }
            return Ok(addresses);
        }
        
        [HttpPost("add-address")]
        public async Task<IActionResult> AddAddress([FromBody] AddressDto addressDto)
        {
            var userPrincipal = User;
            var result = await _addressService.AddAddressAsync(addressDto, userPrincipal);
            if (result)
            {
                return Ok(new { success = true, message = "Address added successfully." });
            }
            else
            {
                return BadRequest(new { success = false, message = "Failed to add address." });
            }
        }
        [HttpGet("get-orders")]
        public async Task<IActionResult> GetOrders()
        {
            var userPrincipal = User;
            var orders = await _customerService.GetCustomerOrders(userPrincipal);
            if (orders == null)
            {
                return NotFound("Customer's Addresses not found.");
            }
            return Ok(orders);
        }

        [HttpGet("customer-unreviewed-orders/{id}")]
        public async Task<IActionResult> GetCustomerUnReviewedOrders(string id)
        {
            var data = await _orderService.GetUnReviewedOrdersByCustomerIdAsync(id);

            if (data == null) return Ok(new List<OrderReviewDto>());

            return Ok(data);
        }
    }
}
