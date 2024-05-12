using System.Security.Claims;
using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using MeallyExtended.Business.Operations;
using MeallyExtended.Contracts.Dto;
using MeallyExtended.Contracts.Requests.Recipe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

namespace MeallyExtended.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IPopularityService _popularityService;

        public RecipeController(IRecipeService recipeService, IPopularityService popularityService)
        {
            _recipeService = recipeService;
            _popularityService = popularityService;
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

        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularRecipes([FromQuery] int amount = 5)
        {
            var result = await _popularityService.GetPopularRecipes(amount);

            return Ok(result.Select(MeallyMapper.RecipeToDto).ToList());
        }

        [HttpGet("browse")]
        public async Task<IActionResult> BrowseRecipes([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _recipeService.GetBrowseRecipes(pageNo, pageSize);

            return Ok(result);
        }

        [HttpGet("suggestion")]
        public async Task<IActionResult> GetSearchSuggestions([FromQuery] string query, [FromQuery] int amount = 5)
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
        public async Task<IActionResult> LikeRecipe([FromBody] Guid recipeId)
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            await _recipeService.LikeRecipe(recipeId, userEmail);

            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateRecipe([FromBody] UpdateRecipeRequest request)
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            try
            {
                var result = await _recipeService.UpdateRecipe(request, userEmail);


                if (result is null)
                {
                    return BadRequest();
                }

                return Ok(MeallyMapper.RecipeToDto(result));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
            }
        }
    }
}
