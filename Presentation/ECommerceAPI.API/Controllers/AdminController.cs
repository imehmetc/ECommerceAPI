using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Persistence.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ECommerceAPI.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("approved-sellers")]
        public async Task<IActionResult> GetApprovedSellers()
        {
            try
            {
                var sellers = await _adminService.GetApprovedSellersAsync();
                return Ok(sellers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("pending-sellers")]
        public async Task<IActionResult> GetPendingSellers()
        {
            try
            {
                var sellers = await _adminService.GetPendingSellersAsync();
                return Ok(sellers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("approve-seller")]
        public async Task<IActionResult> ApproveSeller([FromBody] string sellerId)
        {
            var result = await _adminService.ApproveSellerAsync(sellerId);
            if (result.Succeeded)
            {
                return Ok("Seller approved successfully.");
            }
            return BadRequest("Failed to approve seller.");
        }

        [HttpPost("unapprove-seller")]
        public async Task<IActionResult> UnapproveSeller([FromBody] string sellerId)
        {
            var result = await _adminService.UnapproveSellerAsync(sellerId);
            if (result.Succeeded)
            {
                return Ok("Seller unapproved successfully.");
            }
            return BadRequest("Failed to unapprove seller.");
        }

        [HttpPost("remove-seller")]
        public async Task<IActionResult> RemoveSeller([FromBody] string sellerId)
        {
            var result = await _adminService.RemoveSellerAsync(sellerId);
            if (result.Succeeded)
            {
                return Ok("Seller removed successfully.");
            }
            return BadRequest("Failed to remove seller.");
        }

        [HttpPost("reject-seller")]
        public async Task<IActionResult> RejectSeller([FromBody] string sellerId)
        {
            var result = await _adminService.RejectSellerAsync(sellerId);
            if (result.Succeeded)
            {
                return Ok("Seller rejected successfully.");
            }
            return BadRequest("Failed to reject seller.");
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userPrincipal = User;
            var adminDto = await _adminService.GetProfileAsync(userPrincipal);
            if (adminDto == null)
            {
                return NotFound("Admin not found.");
            }
            return Ok(adminDto);
        }

        [HttpPost("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] AdminDto adminDto, [FromForm] IFormFile? photoUrl)
        {
            if (adminDto == null)
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

                adminDto.Photo = photoUrl.FileName;
            }
            var userPrincipal = User;
            var result = await _adminService.UpdateProfileAsync(adminDto, userPrincipal);

            if (result.Succeeded)
            {
                return Ok("Profile updated successfully.");
            }
            return BadRequest("Failed to update profile.");
        }

        [HttpGet("helpcenter")]
        public async Task<IActionResult> GetHelpCenters()
        {
            var helpCenters = await _adminService.GetHelpCentersAsync();
            return Ok(helpCenters);
        }
        

        [HttpPost("add-helpcenter")]
        public async Task<IActionResult> AddHelpCenter( HelpCenterDto helpCenterDto)
        {
            if (helpCenterDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _adminService.AddHelpCenterAsync(helpCenterDto);

            if (result == true)
            {
                return Ok("Popular question added successfully.");
            }
            return BadRequest("Failed to add popular question.");
        }
        [HttpGet("get-helpcenter")]
        public async Task<IActionResult> GetHelpCenter(string id)
        {

            var result = await _adminService.GetHelpCenterAsync(id);

            if (result !=null )
            {
                return Ok(result);
            }
            return BadRequest("Failed to add popular question.");
        }
        [HttpPost("update-helpcenter")]
        public async Task<IActionResult> UpdateHelpCenter(string helpCenterId,HelpCenterDto helpCenterDto)
        {
            if (helpCenterDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _adminService.UpdateHelpCenterAsync(helpCenterId,helpCenterDto);

            if (result == true)
            {
                return Ok("Popular question updated successfully.");
            }
            return BadRequest("Failed to update popular question.");
        }
        [HttpPost("remove-helpcenter")]
        public async Task<IActionResult> RemoveHelpCenter( string helpCenterId, HelpCenterDto helpCenterDto)
        {
            var result = await _adminService.RemoveHelpCenterAsync(helpCenterId,helpCenterDto);

            if (result == true)
            {
                return Ok("Populer question removed successfully.");
            }
            return BadRequest("Failed to remove populer question.");
        }
        [HttpGet("aboutus")]
        public async Task<IActionResult> GetAboutUs()
        {
            var aboutUs = await _adminService.GetAboutUsAsync();
            return Ok(aboutUs);
        }
        [HttpPost("update-aboutus")]
        public async Task<IActionResult> UpdateAboutUs(string aboutUsId, AboutUsDto aboutUsDto)
        {
            if (aboutUsDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _adminService.UpdateAboutUsAsync(aboutUsId, aboutUsDto);

            if (result == true)
            {
                return Ok("About us updated successfully.");
            }
            return BadRequest("Failed to update about us.");
        }
        [HttpGet("contact")]
        public async Task<IActionResult> GetContact()
        {
            var contact = await _adminService.GetContactAsync();
            return Ok(contact);
        }
        [HttpPost("update-contact")]
        public async Task<IActionResult> UpdateContact( string contactId, ContactDto contactDto)
        {
            if (contactDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _adminService.UpdateContactAsync(contactId, contactDto);

            if (result == true)
            {
                return Ok("Contact updated successfully.");
            }
            return BadRequest("Failed to update contact.");
        }


        // Admin Raporları

        [HttpGet("monthly-registrations")]
        public async Task<IActionResult> GetMonthlyUserCounts()
        {
            var data = await _adminService.MonthlyCountOfUsersAsync();

            if(data != null)
                return Ok(data);

            return BadRequest();
        }

        [HttpGet("category-product-counts")]
        public async Task<IActionResult> GetCategoryProductCounts()
        {
            var data = await _adminService.GetCategoryProductCountsAsync();
            
            if (data != null)
                return Ok(data);

            return BadRequest();
        }

        [HttpGet("stock-levels")]
        public async Task<IActionResult> GetStockLevels()
        {
            var data = await _adminService.GetStockLevelsAsync();

            if (data != null)
                return Ok(data);

            return BadRequest();
        }

        [HttpGet("admin-reports")]
        public async Task<IActionResult> GetAdminReports()
        {
            ClaimsPrincipal claimsPrincipal = User;
            var data = await _adminService.ReportsAsync(claimsPrincipal);

            if (data == null) return Ok(new AdminReportDto());

            return Ok(data);
        }

    }
}
