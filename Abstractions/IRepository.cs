
using System.Linq.Expressions;

namespace GenericRepositoryPattern.Abstractions
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TResult>> FindAllAsync<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector);
        Task<TResult?> FindAsync<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> filter);
        Task<IQueryable<TResult>> Queryable<TResult>(Expression<Func<TEntity, TResult>> selector);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}