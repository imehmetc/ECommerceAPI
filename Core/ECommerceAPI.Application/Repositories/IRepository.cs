using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IRepository<T> where T : class
    {

        void LazyLoadingChangeState(bool State);

        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, List<Expression<Func<T, object>>> includes = null);
        Task<IReadOnlyList<T>> GetAllPagingAsync(Expression<Func<T, bool>> predicate, int number, int size);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                       List<Expression<Func<T, object>>> includes = null,
                                       bool disableTracking = true);
        Task<IQueryable<T>> GetQuery();
        Task<T> GetByIdAsync(string id, List<Expression<Func<T, object>>> includes = null);
        Task<T> GetByQueryAsync(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);

        Task DeleteAsync(T entity);


        Task<IDbContextTransaction> BeginTransaction();

        IQueryable<T> GetListByIncludes(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        Task GetOrderByIdAsync(int orderId);
        Task<IEnumerable<object>> GetOrdersByCustomerIdAsync(int customerId);
    }

}
