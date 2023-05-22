using AutoMapper;
using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.Interfaces;
using EcommerceAPI.Domain.Interfaces;

namespace EcommerceAPI.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDTO> GetAsync(Guid id)
        {
            return _mapper.Map<OrderDTO>(await _orderRepository.GetAsync(id));
        }
    }
}
