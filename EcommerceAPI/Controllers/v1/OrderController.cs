using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.Interfaces;
using EcommerceAPI.Controllers.Shared;
using EcommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EcommerceAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class OrderController : ApiControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get order by id")]
        public async Task<ActionResult<OrderDTO>> Get(Guid id)
        {
            var order = await _orderService.GetAsync(id);

            return order is not null
                ? Ok(order)
                : NoContent();
        }
    }
}
