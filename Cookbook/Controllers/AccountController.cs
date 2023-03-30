using Cookbook.Services;
using Microsoft.AspNetCore.Mvc;
using Cookbook.Models.Contracts;
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
        //[ProducesResponseType(typeof(SaveUserContract.Response),200)]
        public async Task<IActionResult> SaveUser([FromBody] SaveUserContract.Request request)
        {
            try
            {
                var response = await _recipeService.SaveUserAsync(request);
                return Ok(response);
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
