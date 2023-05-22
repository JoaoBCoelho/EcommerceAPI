using AutoMapper;
using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.Interfaces;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Exceptions;
using EcommerceAPI.Domain.Interfaces;

namespace EcommerceAPI.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository,
            ICartRepository cartRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<OrderDTO> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDTO> PlaceOrderAsync(Guid cartId, CheckoutDTO checkoutDTO)
        {
            var cart = await _cartRepository.GetAsync(cartId);
            if (cart is null)
            {
                throw new NotFoundException("The cart was not found.");
            }

            Order order = _mapper.Map<Order>(checkoutDTO);

            return new OrderDTO();
        }
    }
}
