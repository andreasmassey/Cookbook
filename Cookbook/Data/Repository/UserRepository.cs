using Cookbook.Models.Entities;

namespace Cookbook.Data.Repository
{
    public class UserRepository : EntityBaseRepository<UserEntity>, IUserRepository
    {
        private CookbookContext _context;

        public UserRepository(CookbookContext context) : base(context)
        {
            _context = context;
        }
    }
}
