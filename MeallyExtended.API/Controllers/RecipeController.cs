using System.Security.Claims;
using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using MeallyExtended.Contracts.Requests.Recipe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeRequest request)
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var recipe = MeallyMapper.CreateRecipeRequestToRecipeDto(request, userEmail);
            var result = await _recipeService.AddRecipe(recipe);

            return CreatedAtAction("GetRecipe", new { recipeId = result.Id }, MeallyMapper.RecipeToDto(result));
        }

        [HttpGet("{recipeId}:guid")]
        public async Task<IActionResult> GetRecipe(Guid recipeId)
        {
            
            return BadRequest();

        }
    }
}
