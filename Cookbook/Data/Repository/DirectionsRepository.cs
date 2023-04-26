using Cookbook.Models.Entities;

namespace Cookbook.Data.Repository
{
    public class DirectionsRepository : EntityBaseRepository<DirectionEntity>, IDirectionsRepository
    {
        private CookbookContext _context;

        public DirectionsRepository(CookbookContext context) : base(context)
        {
            _context = context;
        }
    }
}
