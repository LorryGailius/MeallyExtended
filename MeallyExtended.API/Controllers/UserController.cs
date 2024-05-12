using System.Security.Claims;
using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeallyExtended.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavoriteRecipes(int pageNo = 1, int pageSize = 10)
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var result = await _userService.GetFavoriteRecipes(userEmail, pageNo, pageSize);

            return Ok(result);
        }
    }
}
