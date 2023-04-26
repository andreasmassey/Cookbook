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

        [HttpPost("v1/recipe")]
        [ProducesResponseType(typeof(GetSpecificRecipeContract.GetSpecificRecipeResponse),200)]
        public async Task<IActionResult> GetSpecificRecipe([FromBody] GetSpecificRecipeContract.GetSpecificRecipeRequest request)
        {
            try
            {
                var response = await _recipeService.GetSpecificRecipeAsync(request);
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
