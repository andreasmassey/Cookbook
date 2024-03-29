﻿using Cookbook.Models.Contracts;
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

        [HttpGet("v1/recipe/{id}")]
        [ProducesResponseType(typeof(GetSpecificRecipeContract.GetSpecificRecipeResponse), 200)]
        public async Task<IActionResult> GetSpecificRecipe(long id)
        {
            try
            {
                var response = await _recipeService.GetSpecificRecipeAsync(new GetSpecificRecipeContract.GetSpecificRecipeRequest { RecipeID = id});
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

        [HttpPost("v1/recipe")]
        [ProducesResponseType(typeof(CreateRecipeContract.CreateRecipeResponse), 200)]
        public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeContract.CreateRecipeRequest request)
        {
            try
            {
                var response = await _recipeService.CreateRecipeAsync(request);
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
