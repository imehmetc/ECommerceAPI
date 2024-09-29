using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions.Token
{
	public interface IJwtTokenService
	{
		Task<string> GenerateTokenAsync(AppUser user);
	}
}
