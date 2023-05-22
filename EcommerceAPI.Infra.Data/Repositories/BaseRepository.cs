using EcommerceAPI.Infra.Data.Context;

namespace EcommerceAPI.Infra.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
