using Cookbook.Models.Contracts;
using Cookbook.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cookbook.Controllers
{
    [Route("api/recipe")]
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("v1/recipes")]
        //[ProducesResponseType(typeof(GetRecipesContract.Response),200)]
        public async Task<IActionResult> Recipes()
        {
            try
            {
                var response = await _recipeService.GetAllRecipesAsync();

                return Ok(response);
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
