using GenericRepositoryPattern.Business.Interfaces;
using GenericRepositoryPattern.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepositoryPattern.Controllers
{
    [ApiController]
    [Route("api/articles")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [ProducesResponseType(type: typeof(IEnumerable<ArticleDto>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetArticles()
        {
            var result = await _articleService.GetArticles();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof(ArticleDto), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetArticleById(Guid id)
        {
            var result = await _articleService.GetArticleById(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(string), statusCode: StatusCodes.Status200OK)]
        public IActionResult Create(ArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            _articleService.Add(request);

            return Ok("Article added successfully");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(type: typeof(string), statusCode: StatusCodes.Status200OK)]
        public IActionResult Update(Guid id, ArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            _articleService.Update(id, request);
            return Ok("Article updated successfully");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(type: typeof(string), statusCode: StatusCodes.Status200OK)]
        public IActionResult Delete(Guid id)
        {
            _articleService.Remove(id);
            return Ok("Article deleted successfully");
        }
    }
}