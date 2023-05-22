using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Interfaces;
using EcommerceAPI.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Infra.Data.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetAsync(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }
    }
}
