using GenericRepositoryPattern.DTOs;

namespace GenericRepositoryPattern.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        void Add(CategoryRequest category);
    }
}