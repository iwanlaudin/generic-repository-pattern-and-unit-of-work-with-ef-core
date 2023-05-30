
namespace GenericRepositoryPattern.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;
    }
}