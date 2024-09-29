using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Persistence.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.API.Controllers
{
    [Authorize(Roles = "Member")]
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }
        [HttpPost("add-cart")]
        public async Task<IActionResult> AddProductToCart([FromBody] CartItemDto cartItemDto)
        {
            // Kullanıcının kimliğini al
            var user = User;

            // Servis katmanındaki metodu çağırarak sepete ürün ekle
            var success = await _cartItemService.AddProductToCart(cartItemDto, user);

            // Eğer ürün sepete başarılı bir şekilde eklendiyse, kullanıcıyı sepet sayfasına yönlendir
            
            return Ok(); // Sepet sayfasına yönlendirme
            
        }
    }
}
