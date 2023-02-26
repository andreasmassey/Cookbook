using Cookbook.Data.Repository;
using Cookbook.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<IList<RecipeEntity>> GetAllRecipesAsync()
        {
            var response = await _recipeRepository.GetAllAsync();
            //var test = await _recipeRepository.GetAllAsync(x => x.DateCreated > DateTime.Now.AddMonths(-2)); //using a predicate/where clause
            return response.ToList();
        }
    }
}
