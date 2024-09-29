using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            //services.AddDbContext<ECommerceDbContext>(options => options.UseSqlServer("DefaultConnection"));
        }
    }
}
