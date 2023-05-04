using Cookbook.Models.Entities;

namespace Cookbook.Data.Repository
{
    public class IngredientsRepository : EntityBaseRepository<IngredientEntity>, IIngredientsRepository
    {
        private CookbookContext _context;

        public IngredientsRepository(CookbookContext context) : base(context)
        {
            _context = context;
        }
    }
}
