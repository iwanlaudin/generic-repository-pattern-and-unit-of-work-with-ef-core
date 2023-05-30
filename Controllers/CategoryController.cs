using GenericRepositoryPattern.Business.Interfaces;
using GenericRepositoryPattern.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepositoryPattern.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(type: typeof(IEnumerable<CategoryDto>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryService.GetCategories();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(string), statusCode: StatusCodes.Status200OK)]
        public IActionResult Create(CategoryRequest category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }
            _categoryService.Add(category);
            return Ok("Category added successfully");
        }
    }
}