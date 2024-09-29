using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Persistence.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [Authorize(Roles = "Seller")]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userPrincipal = User;
            var sellerDto = await _sellerService.GetProfileAsync(userPrincipal);
            if (sellerDto == null)
            {
                return NotFound("Seller not found.");
            }
            return Ok(sellerDto);
        }

        [Authorize(Roles = "Seller")]
        [HttpPost("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] SellerDto sellerDto, [FromForm] IFormFile? photoUrl)
        {
            if (sellerDto == null)
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

                sellerDto.Photo = photoUrl.FileName;
            }
            var userPrincipal = User;
            var result = await _sellerService.UpdateProfileAsync(sellerDto, userPrincipal);

            if (result.Succeeded)
            {
                return Ok("Profile updated successfully.");
            }
            return BadRequest("Failed to update profile.");
        }

        [Authorize(Roles = "Seller")]
        [HttpGet("seller-products/{id}")]
        public async Task<IActionResult> GetSellerProducts(string id)
        {
            var sellerProducts = await _sellerService.GetSellerProducts(id);

            if (sellerProducts != null) return Ok(sellerProducts);

            return BadRequest();
        }
 
        [Authorize(Roles = "Seller")]
        [HttpGet("ordered-products")]
        public async Task<IActionResult> GetOrderedProducts()
        {
            var userPrincipal = User;
            var sellerOrders = await _sellerService.GetCartItemsAsync(userPrincipal);

            if (sellerOrders != null) return Ok(sellerOrders);

            return BadRequest();
        }


        [Authorize(Roles = "Seller")]
        [HttpPost("seller-reply")]
        public async Task<IActionResult> SellerReply([FromBody] SellerReplyDto sellerReplyDto)
        {
            if (sellerReplyDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _sellerService.AddSellerReplyAsync(sellerReplyDto);

            if (result) return Ok();

            return BadRequest();
        }

        [HttpGet("seller-replies/{id}")]
        public async Task<IActionResult> GetSellerReplies(string id)
        {
            var data = await _sellerService.GetSellerRepliesByIdAsync(id);

            if (data != null) return Ok(data);

            return BadRequest();
        }

        [Authorize(Roles = "Seller")]
        [HttpGet("seller-reports")]
        public async Task<IActionResult> GetSellerReports()
        {
            ClaimsPrincipal claimsPrincipal = User;
            var data = await _sellerService.ReportsAsync(claimsPrincipal);

            if (data == null) return Ok(new SellerReportDto());

            return Ok(data);
        }

        [Authorize(Roles = "Seller")]
        [HttpGet("seller-stock-levels")]
        public async Task<IActionResult> GetStockLevels()
        {
            ClaimsPrincipal claimsPrincipal = User;
            var data = await _sellerService.GetStockLevelsAsync(claimsPrincipal);

            if (data == null) return Ok(new List<ProductStockDto>());

            return Ok(data);
        }

        [HttpGet("get-brands")]
        public async Task<ActionResult<List<SellerForBrandDto>>> GetAllSellers()
        {
            var sellers = await _sellerService.GetAllSellers();
            if (sellers != null)
            {
                return Ok(sellers);
            }
            return NoContent();
        }
    }
}
