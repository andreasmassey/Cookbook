using Cookbook.Models.Entities;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cookbook.Data.Repository
{
    public interface IRecipeRepository : IEntityBaseRepository<RecipeEntity>
    {
    }
}
