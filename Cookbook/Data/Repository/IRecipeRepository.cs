using Cookbook.Models.Entities;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Cookbook.Models;

namespace Cookbook.Data.Repository
{
    public interface IRecipeRepository : IEntityBaseRepository<RecipeEntity>
    {
        Task<Recipes> GetRecipesAsync(long userId);
        Task<DirectionModel> GetDirectionsAsync(long recipeId);
        Task<IngredientModel> GetIngredientsAsync(long recipeId);
    }
}
