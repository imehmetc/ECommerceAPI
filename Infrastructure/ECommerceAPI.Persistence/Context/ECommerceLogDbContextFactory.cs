using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Context
{
    public class ECommerceLogDbContextFactory : IDesignTimeDbContextFactory<ECommerceLogDbContext>
    {
        public ECommerceLogDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ECommerceLogDbContext>();
            var connectionString = configuration.GetConnectionString("LogConnection");

            builder.UseSqlServer(connectionString);

            return new ECommerceLogDbContext(builder.Options);
        }
    }
}
