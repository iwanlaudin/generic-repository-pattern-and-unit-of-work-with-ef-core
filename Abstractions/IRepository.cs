
using System.Linq.Expressions;

namespace GenericRepositoryPattern.Abstractions
{
    public interface IRepository<TEntity, TResult> 
        where TEntity : class 
        where TResult : class
    {
        Task<IEnumerable<TResult>> FindAllAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector);
        Task<TResult?> FindAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector);
        Task<IQueryable<TResult>> Queryable(Expression<Func<TEntity, TResult>> selector);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}