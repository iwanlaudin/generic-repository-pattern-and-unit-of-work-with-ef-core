using System.Linq.Expressions;
using GenericRepositoryPattern.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GenericRepositoryPattern.Repositories
{
    public class Repository<TEntity, TResult> : IRepository<TEntity, TResult>
        where TEntity : class
        where TResult : class
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TResult>> FindAllAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector)
        {
            var result = await _dbContext.Set<TEntity>()
                .Where(filter)
                .Select(selector)
                .ToListAsync();

            return result;
        }

        public async Task<TResult?> FindAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector)
        {
            TResult? result = await _dbContext.Set<TEntity>()
                .Where(filter)
                .Select(selector)
                .FirstOrDefaultAsync();

            return result;
        }

        public void Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public Task<IQueryable<TResult>> Queryable(Expression<Func<TEntity, TResult>> selector)
        {
            IQueryable<TResult> query = _dbContext.Set<TEntity>().Select(selector);
            return Task.FromResult(query);
        }
    }
}