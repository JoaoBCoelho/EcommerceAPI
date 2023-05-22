using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Interfaces;
using EcommerceAPI.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Infra.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync()
        {
            var cart = new Cart();

            await _context.AddAsync(cart);
            await _context.SaveChangesAsync();

            return cart.Id;
        }

        public async Task<Cart> GetAsync(Guid id)
        {
            return await _context.Carts
                .Include(i => i.CartProducts)
                .ThenInclude(i => i.Product)
                .ThenInclude(i=> i.Category)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task UpdateAsync(Cart cart)
        {
            _context.Update(cart);
            await _context.SaveChangesAsync();
        }
    }
}
