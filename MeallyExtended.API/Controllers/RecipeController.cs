using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using MeallyExtended.Contracts.Requests.Recipe;
using Microsoft.AspNetCore.Mvc;

namespace MeallyExtended.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeRequest request)
        //{
        //    var recipe = MeallyMapper.MapCreateRecipeRequestToDto(request);
        //    var result = _recipeService.AddRecipe(recipe);

        //    return CreatedAtAction("GetRecipe", new { recipeId = result.Id }, result);
        //}

        [HttpGet("{recipeId}:guid")]
        public async Task<IActionResult> GetRecipe(Guid recipeId)
        {
            
            return BadRequest();

        }
    }
}
