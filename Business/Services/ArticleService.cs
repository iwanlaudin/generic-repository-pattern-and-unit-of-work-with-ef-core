using System.Linq.Expressions;
using GenericRepositoryPattern.Abstractions;
using GenericRepositoryPattern.Business.Interfaces;
using GenericRepositoryPattern.DTOs;
using GenericRepositoryPattern.Entities;

namespace GenericRepositoryPattern.Business.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Article> _articleRepository;
        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _articleRepository = _unitOfWork.GetRepository<Article>();
        }

        public async Task<IEnumerable<ArticleDto>> GetArticles()
        {
            Expression<Func<Article, bool>> filter = a =>
                a.DeletedAt == null && a.Category.DeletedAt == null;
            Expression<Func<Article, ArticleDto>> selector = a => new ArticleDto
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                PublishedDate = a.PublishedDate,
                Category = new CategoryDto
                {
                    Id = a.Category.Id,
                    Name = a.Category.Name,
                    CreatedAt = a.Category.CreatedAt,
                    UpdatedAt = a.Category.UpdatedAt
                },
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            };

            var articles = await _articleRepository.FindAllAsync(filter, selector);

            return articles;
        }

        public async Task<ArticleDto?> GetArticleById(Guid id)
        {
            Expression<Func<Article, bool>> filter = a =>
                a.Id == id && a.DeletedAt == null && a.Category.DeletedAt == null;
            Expression<Func<Article, ArticleDto>> selector = a => new ArticleDto
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                PublishedDate = a.PublishedDate,
                Category = new CategoryDto
                {
                    Id = a.Category.Id,
                    Name = a.Category.Name,
                    CreatedAt = a.Category.CreatedAt,
                    UpdatedAt = a.Category.UpdatedAt
                },
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            };

            var article = await _articleRepository.FindAsync(filter, selector);
            return article;
        }

        public void Add(ArticleRequest request)
        {
            var articleEntity = new Article
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content,
                CategoryId = request.CategoryId
            };

            if (request.isPublished)
            {
                articleEntity.PublishedDate = DateTime.UtcNow;
            }

            _articleRepository.Insert(articleEntity);
            _unitOfWork.SaveChangesAsync();
        }

        public void Update(Guid id, ArticleRequest request)
        {
            var article = _articleRepository.FindAsync(a => a.Id == id).Result;
            if (article is null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            article.Title = request.Title;
            article.Content = request.Content;
            article.CategoryId = request.CategoryId != null ? request.CategoryId : article.CategoryId;
            article.PublishedDate = request.isPublished ? DateTime.UtcNow : article.PublishedDate;

            _articleRepository.Update(article);
            _unitOfWork.SaveChangesAsync();
        }

        public void Remove(Guid id)
        {
            var article = _articleRepository.FindAsync(a => a.Id == id).Result;
            if (article is null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            article.DeletedAt = DateTime.UtcNow;
            _articleRepository.Update(article);
            _unitOfWork.SaveChangesAsync();
        }
    }
}