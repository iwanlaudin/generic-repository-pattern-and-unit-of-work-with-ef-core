
using GenericRepositoryPattern.DTOs;
using GenericRepositoryPattern.Entities;

namespace GenericRepositoryPattern.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        void Add(CategoryRequest category);
    }
}