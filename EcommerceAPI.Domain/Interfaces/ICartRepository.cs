using EcommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<Guid> CreateAsync();
        Task<Cart> GetAsync(Guid id);
        Task UpdateAsync(Cart cart);
    }
}
