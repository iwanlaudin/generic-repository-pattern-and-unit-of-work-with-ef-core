using System.Linq.Expressions;
using GenericRepositoryPattern.Abstractions;
using GenericRepositoryPattern.Business.Interfaces;
using GenericRepositoryPattern.DTOs;
using GenericRepositoryPattern.Entities;

namespace GenericRepositoryPattern.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = _unitOfWork.GetRepository<Category>();
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            Expression<Func<Category, bool>> filter = a => a.DeletedAt == null;
            Expression<Func<Category, CategoryDto>> selector = a => new CategoryDto
            {
                Id = a.Id,
                Name = a.Name,
                CreatedAt = a.CreatedAt
            };

            var categorys = await _categoryRepository.FindAllAsync(filter, selector);
            return categorys;
        }

        public void Add(CategoryRequest category)
        {
            var categoryEntity = new Category
            {
                Id = Guid.NewGuid(),
                Name = category.Name,
                CreatedAt = DateTime.Now,
            };

            _categoryRepository.Insert(categoryEntity);
            _unitOfWork.SaveChangesAsync();
        }
    }
}