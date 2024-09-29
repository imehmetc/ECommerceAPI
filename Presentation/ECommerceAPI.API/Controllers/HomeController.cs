using ECommerceAPI.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IAdminService _adminService;

        public HomeController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("get-helpcenter")]
        public async Task<IActionResult> GetHelpCenter()
        {

            var result = await _adminService.GetHelpCentersAsync();

            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Failed to add popular question.");
        }
        [HttpGet("aboutus")]
        public async Task<IActionResult> GetAboutUs()
        {
            var aboutUsList = await _adminService.GetAboutUsAsync();
             var aboutUs = aboutUsList.FirstOrDefault();
            return Ok(aboutUs);
        }
        [HttpGet("contact")]
        public async Task<IActionResult> GetContact()
        {
            var contactList = await _adminService.GetContactAsync();
            var contact = contactList.FirstOrDefault();
            return Ok(contact);
        }


    }
}
