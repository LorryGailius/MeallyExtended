using System.Security.Claims;
using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using MeallyExtended.Contracts.Dto;
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
            
            var result = await _recipeService.GetRecipeById(recipeId);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(MeallyMapper.RecipeToDto(result));
        }

        [HttpDelete("{recipeId}:guid")]
        [Authorize]
        public async Task<IActionResult> DeleteRecipe(Guid recipeId)
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var result = await _recipeService.DeleteRecipe(recipeId, userEmail);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("suggestion")]
        public async Task<IActionResult> GetSearchSuggestions([FromQuery] string query, [FromQuery] int amount)
        {
            var result = await _recipeService.GetSearchSuggestions(query, amount);

            return Ok(result);
        }

        [HttpPost("search")]
        public async Task<ActionResult<PaginationResult<RecipeDto>>> GetRecipesByQuery([FromQuery] string? query, [FromQuery] List<string>? categories, [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _recipeService.GetRecipesByQuery(query, categories, pageNo, pageSize);

            return Ok(result);
        }

        [HttpPost("like")]
        [Authorize]
        public async Task<IActionResult> UpdateRecipe([FromBody] Guid recipeId)
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            await _recipeService.LikeRecipe(recipeId, userEmail);

            return Ok();
        }
    }
}
