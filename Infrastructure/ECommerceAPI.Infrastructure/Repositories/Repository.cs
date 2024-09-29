using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ECommerceAPI.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ECommerceDbContext _dbContext;
        private readonly DbSet<T> _entities;

        public Repository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllPagingAsync(Expression<Func<T, bool>> predicate, int number, int size)
        {
            if (predicate != null)
            {
                return await _dbContext.Set<T>().Where(predicate).Skip(number).Take(size).ToListAsync();
            }
            else
            {
                return await _dbContext.Set<T>().Skip(number).Take(size).ToListAsync();
            }
        }

        public void LazyLoadingChangeState(bool State)
        {
            _dbContext.ChangeTracker.LazyLoadingEnabled = State;
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IQueryable<T>> GetQuery()
        {
            return _dbContext.Set<T>();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);
            return await query.ToListAsync();
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(string id, List<Expression<Func<T, object>>> includes = null)
        {
            var entity = await _dbContext.Set<T>().FindAsync(Guid.Parse(id));

            if (includes != null && entity != null)
            {
                foreach (var include in includes)
                {
                    var navigation = _dbContext.Entry(entity).Navigation(include.GetPropertyAccess().Name);

                    if (navigation.Metadata.IsCollection)
                    {
                        await navigation.LoadAsync();
                    }
                    else
                    {
                        await _dbContext.Entry(entity).Reference(include).LoadAsync();
                    }
                }
            }

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _entities.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
      
        public async Task<T> GetByQueryAsync(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.FirstOrDefault();
        }

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

        public IQueryable<T> GetListByIncludes(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            return query;
        }

        public Task GetOrderByIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<object>> GetOrdersByCustomerIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
