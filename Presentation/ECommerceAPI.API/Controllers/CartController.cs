using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("active-card")]
        public async Task<IActionResult> GetActiveCart()
        {
            var user = User;
            var cartDto = await _cartService.GetActiveCartAsync(user);

            if (cartDto != null)
            {
                return Ok(cartDto);
            }

            return BadRequest();
        }
        [HttpPost("update-cart")]
        public async Task<IActionResult> UpdateCart(CartItemDto cartItemDto)
        {
            
            var cartDto = await _cartService.UpdateCartAsync(cartItemDto);

            if (cartDto != null)
            {
                return Ok(cartDto);
            }

            return BadRequest();
        }
        [HttpPost("remove-cartitem")]
        public async Task<IActionResult> RemoveCartItem(CartItemDto cartItemDto)
        {

            var cartDto = await _cartService.RemoveCartItemAsync(cartItemDto);

            if (cartDto != null)
            {
                return Ok(cartDto);
            }

            return BadRequest();
        }
    }
}
