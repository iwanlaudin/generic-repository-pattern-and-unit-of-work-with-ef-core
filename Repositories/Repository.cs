using System.Linq.Expressions;
using GenericRepositoryPattern.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GenericRepositoryPattern.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TResult>> FindAllAsync<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector)
        {
            IEnumerable<TResult> result = await _dbContext.Set<TEntity>()
                .Where(filter)
                .Select(selector)
                .ToListAsync();

            return result;
        }

        public Task<IQueryable<TResult>> Queryable<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            IQueryable<TResult> query = _dbContext.Set<TEntity>().Select(selector);
            return Task.FromResult(query);
        }

        public async Task<TResult?> FindAsync<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector)
        {
            TResult? result = await _dbContext.Set<TEntity>()
                .Where(filter)
                .Select(selector)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            TEntity? result = await _dbContext.Set<TEntity>()
                .Where(filter)
                .FirstOrDefaultAsync();

            return result;
        }

        public void Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}