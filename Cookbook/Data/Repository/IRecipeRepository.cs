using Cookbook.Models;
using Cookbook.Models.Entities;
using System.Threading.Tasks;

namespace Cookbook.Data.Repository
{
    public interface IRecipeRepository : IEntityBaseRepository<RecipeEntity>
    {
        Task<Recipes> GetOneRecipeAsync(long recipeId);
        Task<Recipes> GetRecipesAsync(long userId);
        Task<DirectionModel> GetDirectionsAsync(long recipeId);
        Task<IngredientModel> GetIngredientsAsync(long recipeId);
    }
}
