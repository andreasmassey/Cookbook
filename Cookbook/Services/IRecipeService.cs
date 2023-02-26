using Cookbook.Models.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.Services
{
    public interface IRecipeService
    {
        Task<IList<RecipeEntity>> GetAllRecipesAsync();
    }
}
