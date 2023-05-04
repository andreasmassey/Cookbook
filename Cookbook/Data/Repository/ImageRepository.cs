using Cookbook.Models.Entities;

namespace Cookbook.Data.Repository
{
    public class ImageRepository : EntityBaseRepository<ImageEntity>, IImageRepository
    {
        private CookbookContext _context;

        public ImageRepository(CookbookContext context) : base(context)
        {
            _context = context;
        }
    }
}
