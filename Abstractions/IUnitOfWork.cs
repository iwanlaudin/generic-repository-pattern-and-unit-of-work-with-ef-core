
namespace GenericRepositoryPattern.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IRepository<TEntity, TResult> GetRepository<TEntity, TResult>()
            where TEntity : class
            where TResult : class;
    }
}