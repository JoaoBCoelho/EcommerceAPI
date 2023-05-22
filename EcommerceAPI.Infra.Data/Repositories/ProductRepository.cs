using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Interfaces;
using EcommerceAPI.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AllExistAsync(IEnumerable<Guid> productIds)
        {
            return await _context.Products.AllAsync(p => productIds.Contains(p.Id));
        }

        public async Task CreateAsync(Product product)
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.RelatedProducts)
                .ThenInclude(p=> p.RelatedProduct)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.RelatedProducts)
                .ToListAsync();
        }

        public async Task UpdateAsync(Product product)
        {

            _context.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
