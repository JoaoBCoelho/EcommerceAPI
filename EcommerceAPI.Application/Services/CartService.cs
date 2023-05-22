using AutoMapper;
using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.Interfaces;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Exceptions;
using EcommerceAPI.Domain.Interfaces;

namespace EcommerceAPI.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public CartService(ICartRepository cartRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync()
        {
            return await _cartRepository.CreateAsync();
        }

        public async Task<CartDTO> GetAsync(Guid id)
        {
            var cart = await _cartRepository.GetAsync(id);
            return _mapper.Map<CartDTO>(await _cartRepository.GetAsync(id));
        }

        public async Task AddToCartAsync(Guid id, Guid productId, int quantity)
        {
            var cart = await _cartRepository.GetAsync(id);
            await ValidateCartAndProductExistence(cart, productId);

            cart.AddProduct(productId, quantity);
            await _cartRepository.UpdateAsync(cart);
        }

        public async Task RemoveFromCartAsync(Guid id, Guid productId)
        {
            var cart = await _cartRepository.GetAsync(id);
            await ValidateCartAndProductExistence(cart, productId);

            cart.RemoveProduct(productId);
            await _cartRepository.UpdateAsync(cart);
        }

        public async Task UpdateCartAsync(Guid id, Guid productId, int quantity)
        {
            var cart = await _cartRepository.GetAsync(id);
            await ValidateCartAndProductExistence(cart, productId);

            cart.UpdateProductQuantity(productId, quantity);
            await _cartRepository.UpdateAsync(cart);
        }

        private async Task ValidateCartAndProductExistence(Cart cart, Guid productId)
        {
            if (cart is null)
            {
                throw new NotFoundException("The informed cart was not found.");
            }

            Product product = await _productRepository.GetAsync(productId);
            if (product is null)
            {
                throw new NotFoundException("The informed product was not found.");
            }
        }

    }
}
