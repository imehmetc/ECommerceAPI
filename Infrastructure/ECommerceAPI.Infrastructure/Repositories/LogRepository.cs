using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Repositories
{
    public class LogRepository<T> : ILogRepository<T> where T : class
    {
        private readonly ECommerceLogDbContext _logDbContext;
        private readonly DbSet<T> _entities;

        public LogRepository(ECommerceLogDbContext logDbContext)
        {
            _logDbContext = logDbContext;
            _entities = _logDbContext.Set<T>();
        }
        public async Task<IReadOnlyList<T>> GetAllLogsAsync()
        {
            return await _entities.ToListAsync();
        }
    }
}
