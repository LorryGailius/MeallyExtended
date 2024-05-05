using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeallyExtended.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {

        [HttpPost(Name = "CreateRecipe")]
        public async Task<IActionResult> CreateRecipe()
        {
            return BadRequest();
            
        }

        [HttpGet("{recipeId}")]
        public async Task<IActionResult> GetRecipe(Guid recipeId)
        {
            
            return BadRequest();

        }
    }
}
