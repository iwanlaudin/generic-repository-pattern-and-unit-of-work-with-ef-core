
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

        public Task<IEnumerable<CategoryDto>> GetCategories()
        {
            throw new NotImplementedException();
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