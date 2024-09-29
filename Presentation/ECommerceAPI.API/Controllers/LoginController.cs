using ECommerceAPI.API.StaticMethods;
using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly ILoginService _loginService;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IJwtTokenService _jwtTokenService;

		public LoginController(UserManager<AppUser> userManager, ILoginService loginService, SignInManager<AppUser> signInManager, IJwtTokenService jwtTokenService)
		{
			_userManager = userManager;
			_loginService = loginService;
			_signInManager = signInManager;
			_jwtTokenService = jwtTokenService;
		}

		[HttpPost("user-login")]
		public async Task<IActionResult> LoginAdmin([FromBody] LoginDto loginDto)
		{
			var user = await _loginService.LoginAsync(loginDto);

			var result =  await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

			if (result.Succeeded)
			{
				var token = await _jwtTokenService.GenerateTokenAsync(user);
				return Ok(token);
			}

			return BadRequest();
		}

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto googleLoginDto)
        {
			string token = await _loginService.LoginWithGoogle(googleLoginDto);

			return Ok(token);
        }

    }
}
