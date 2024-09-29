using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            // Admin rolü
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new AppRole("Admin"));
            }

            // Satıcı rolü
            if (!await roleManager.RoleExistsAsync("Seller"))
            {
                await roleManager.CreateAsync(new AppRole("Seller"));
            }

            // Üye rolü
            if (!await roleManager.RoleExistsAsync("Member"))
            {
                await roleManager.CreateAsync(new AppRole("Member"));
            }
        }
    }
}
