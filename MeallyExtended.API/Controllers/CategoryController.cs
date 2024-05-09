using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using MeallyExtended.Contracts.Requests.Category;
using Microsoft.AspNetCore.Mvc;

namespace MeallyExtended.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{categoryName}")]
        public async Task<IActionResult> GetCategory(string categoryName)
        {
            var category = await _categoryService.GetCategoryByName(categoryName);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
    }
}