using FoodApplication.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetById(int id);
        IQueryable<T> GetByIdWithTracking(int id);
        IQueryable<T> GetByFilter(Expression<Func<T, bool>> filter);
        IQueryable<T> GetByFilterWithTracking(Expression<Func<T, bool>> filter);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task BulkUpdateAsync(Expression<Func<T, bool>> filter, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression);
        void UpdateInclude(T item, params string[] modifiedProperties);
        Task SoftDeleteAsync(T entity);
        Task SoftDeleteRangeAsync(IEnumerable<T> entities);
        Task BulkSoftDeleteAsync(Expression<Func<T, bool>> filter);
        Task HardDeleteAsync(T entity);
        Task HardDeleteRangeAsync(IEnumerable<T> entities);
        Task BulkHardDeleteAsync(Expression<Func<T, bool>> filter);
        Task SaveChangesAsync();
    }
}
