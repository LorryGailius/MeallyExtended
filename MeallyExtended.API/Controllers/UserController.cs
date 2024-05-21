using System.Security.Claims;
using MeallyExtended.Business.Interfaces;
using MeallyExtended.DataModels.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        /// <summary>
        /// Get favorite recipes for the user. Needs to be authorized.
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("favorites")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetFavoriteRecipes(int pageNo = 1, int pageSize = 10)
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var result = await _userService.GetFavoriteRecipes(userEmail, pageNo, pageSize);

            if (!result.Data.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost("logout")]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        public async Task<IActionResult> Logout(SignInManager<User> signInManager)
        {
            await signInManager.SignOutAsync();
            return SignOut();   
        }
    }
}
