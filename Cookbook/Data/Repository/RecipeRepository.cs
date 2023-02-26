using System.Collections.Generic;
using System.Threading.Tasks;
using Cookbook.Models.Entities;

namespace Cookbook.Data.Repository
{
    public class RecipeRepository : EntityBaseRepository<RecipeEntity>, IRecipeRepository
    {
        private CookbookContext _context;

        public RecipeRepository(CookbookContext context) : base(context)
        {
            _context = context;
        }
    }
}
