using ECommerceAPI.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterMemberAsync(RegisterDto registerDto);
        Task<IdentityResult> RegisterSellerAsync(RegisterSellerDto registerSellerDto);
    }
}
