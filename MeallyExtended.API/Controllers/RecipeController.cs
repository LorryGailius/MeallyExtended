using System.Security.Claims;
using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using MeallyExtended.Contracts.Dto;
using MeallyExtended.Contracts.Requests.Recipe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Create a recipe. Needs to be authorized
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeRequest request)
        {
            if (string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Description) || request.Ingredients.Length == 0 || string.IsNullOrEmpty(request.Instructions))
            {
                return BadRequest("Please fill in all fields");
            }

            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var recipe = MeallyMapper.CreateRecipeRequestToRecipeDto(request, userEmail);
            try
            {
                var result = await _recipeService.AddRecipe(recipe);
                return CreatedAtAction("GetRecipe", new { recipeId = result.Id }, MeallyMapper.RecipeToDto(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get a recipe by id
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [HttpGet("{recipeId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRecipe(Guid recipeId)
        {

            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";

            var result = await _recipeService.GetRecipeById(recipeId);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(MeallyMapper.RecipeToDto(result, userEmail));
        }

        /// <summary>
        /// Delete a recipe. Needs to be authorized and creator of the recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [HttpDelete("{recipeId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> DeleteRecipe(Guid recipeId)
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            try
            {
                await _recipeService.DeleteRecipe(recipeId, userEmail);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get popular recipes
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpGet("popular")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetPopularRecipes([FromQuery] int amount = 5)
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";

            var result = await _popularityService.GetPopularRecipes(amount);

            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result.Select(x => MeallyMapper.RecipeToDto(x, userEmail)));
        }

        /// <summary>
        /// Browse recipes
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("browse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> BrowseRecipes([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";

            var result = await _recipeService.GetBrowseRecipes(userEmail,pageNo, pageSize);

            if (!result.Data.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        /// <summary>
        /// Get autocomplete suggestions based on query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpGet("suggestion")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetSearchSuggestions([FromQuery] string query, [FromQuery] int amount = 5)
        {
            var result = await _recipeService.GetSearchSuggestions(query, amount);

            return Ok(result);
        }

        /// <summary>
        /// Search for recipes based on query and categories
        /// </summary>
        /// <param name="query"></param>
        /// <param name="categories"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost("search")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<PaginationResult<RecipeDto>>> GetRecipesByQuery([FromQuery] string? query, [FromQuery] List<string>? categories, [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";

            var result = await _recipeService.GetRecipesByQuery(query, categories, userEmail, pageNo, pageSize);

            if (!result.Data.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        /// <summary>
        /// Like/Unlike a recipe. Needs to be authorized
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [HttpPost("like")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> LikeRecipe([FromQuery] Guid recipeId)
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            try
            {
                await _recipeService.LikeRecipe(recipeId, userEmail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Update a recipe. Needs to be authorized and creator of the recipe
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> UpdateRecipe([FromBody] UpdateRecipeRequest request)
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            try
            {
                var result = await _recipeService.UpdateRecipe(request, userEmail);

                return Ok(MeallyMapper.RecipeToDto(result));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
