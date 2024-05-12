using MeallyExtended.Business.Interfaces;
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

        [HttpGet("suggestions")]
        public async Task<IActionResult> GetCategoryQuery(string query, int amount = 5)
        {
            var result = await _categoryService.GetQueryCategories(query, amount);

            return Ok(result);
        }
    }
}
