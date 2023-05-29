
using GenericRepositoryPattern.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GenericRepositoryPattern.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, object>();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntity();
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public IRepository<TEntity, TResult> GetRepository<TEntity, TResult>()
            where TEntity : class
            where TResult : class
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IRepository<TEntity, TResult>)_repositories[typeof(TEntity)];
            }

            var repository = new Repository<TEntity, TResult>(_dbContext);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        private void UpdateAuditableEntity()
        {
            IEnumerable<EntityEntry<IAuditableEntity>> entities = _dbContext.ChangeTracker
                .Entries<IAuditableEntity>();

            foreach (EntityEntry<IAuditableEntity> entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Property(x => x.CreatedAt)
                        .CurrentValue = DateTime.UtcNow;
                }

                if (entity.State == EntityState.Modified)
                {
                    entity.Property(x => x.UpdatedAt)
                        .CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }
}