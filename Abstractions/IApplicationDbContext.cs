
using GenericRepositoryPattern.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenericRepositoryPattern.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<Article> Articles { get; set; }
        DbSet<Category> Categories { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}