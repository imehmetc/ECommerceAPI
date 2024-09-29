using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMailService _mailService;

        public AccountController(IAccountService accountService, IMailService mailService)
        {
            _accountService = accountService;
            _mailService = mailService;
        }

        [HttpPost("register/member")]
        public async Task<IActionResult> RegisterMember([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid data", Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            var result = await _accountService.RegisterMemberAsync(registerDto);

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(RegisterMember), new { Message = "Member registered successfully." });
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(new { Message = "Registration failed", Errors = errors });
        }

        [HttpPost("register/seller")]
        public async Task<IActionResult> RegisterSeller([FromBody] RegisterSellerDto registerSellerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid data", Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            var result = await _accountService.RegisterSellerAsync(registerSellerDto);

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(RegisterSeller), new { Message = "Seller registered successfully." });
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(new { Message = "Registration failed", Errors = errors });
        }
    }
}
