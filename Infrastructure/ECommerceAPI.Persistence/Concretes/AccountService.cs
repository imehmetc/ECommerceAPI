using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Concrete;
using ECommerceAPI.Domain.Entities.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Concretes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AccountService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterMemberAsync(RegisterDto registerDto)
        {
            var member = registerDto.Adapt<AppUser>();
            member.Id = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(member, registerDto.Password);
            if (result.Succeeded)
            {  
                await _userManager.AddToRoleAsync(member, "Member");
            }
            return result;
        }

        public async Task<IdentityResult> RegisterSellerAsync(RegisterSellerDto registerSellerDto)
        {
            var seller = registerSellerDto.Adapt<AppUser>();
            seller.Id = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(seller, registerSellerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(seller, "Seller");
            }
            return result;
        }
    }
}
