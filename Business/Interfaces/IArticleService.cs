
using GenericRepositoryPattern.DTOs;

namespace GenericRepositoryPattern.Business.Interfaces
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDto>> GetArticles();
        Task<ArticleDto?> GetArticleById(Guid id);
        void Add(ArticleRequest request);
        void Update(Guid id, ArticleRequest request);
        void Remove(Guid id);
    }
}