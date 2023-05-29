
using GenericRepositoryPattern.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GenericRepositoryPattern.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntity();
            return await _dbContext.SaveChangesAsync(cancellationToken);
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

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}