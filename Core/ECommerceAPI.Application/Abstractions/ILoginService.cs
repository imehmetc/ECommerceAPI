using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions
{	
	public interface ILoginService
	{
		Task<AppUser> LoginAsync(LoginDto loginDto);
		Task<string> LoginWithGoogle(GoogleLoginDto googleLoginDto);
		
	}
}
