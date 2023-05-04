using Cookbook.Models.Contracts;
using Cookbook.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cookbook.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IRecipeService _recipeService;

        public AccountController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost("v1/user")]
        [ProducesResponseType(typeof(CreateUserContract.CreateUserResponse), 200)]
        public async Task<IActionResult> SaveUser([FromBody] CreateUserContract.CreateUserRequest request)
        {
            try
            {
                var response = await _recipeService.SaveUserAsync(request);
                if (!string.IsNullOrEmpty(response?.Error?.ErrorMessage))
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("v1/login")]
        [ProducesResponseType(typeof(UserLoginContract.UserLoginResponse), 200)]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginContract.UserLoginRequest request)
        {
            try
            {
                var response = await _recipeService.UserLoginAsync(request);
                if (!string.IsNullOrEmpty(response?.Error?.ErrorMessage))
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
