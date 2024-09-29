using ECommerceAPI.Domain.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Context
{
    public class ECommerceLogDbContext : DbContext
    {
        public ECommerceLogDbContext(DbContextOptions<ECommerceLogDbContext> options) : base(options)
        {
        }

        public DbSet<LogEntry> LogEntries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogEntry>().ToTable("Logs");
            base.OnModelCreating(modelBuilder);
        }

    }
}
