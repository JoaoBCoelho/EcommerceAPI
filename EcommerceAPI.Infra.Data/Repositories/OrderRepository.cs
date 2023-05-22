using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Interfaces;
using EcommerceAPI.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Infra.Data.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }

        public async Task CreateAsync(Order order)
        {
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetAsync(Guid id)
        {
            return await _context.Orders
                .Include(i => i.ShippingInformation)
                .Include(i => i.BillingInformation)
                .Include(i => i.Cart)
                .ThenInclude(i => i.CartProducts)
                .ThenInclude(i => i.Product)
                .ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
