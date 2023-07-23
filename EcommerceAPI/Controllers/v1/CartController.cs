using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.Interfaces;
using EcommerceAPI.Controllers.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EcommerceAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class CartController : ApiControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get cart by id")]
        public async Task<ActionResult<CartDTO>> Get(Guid id)
        {
            var cart = await _cartService.GetAsync(id);
            return cart is not null
                ? Ok(cart)
                : NoContent();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new cart")]
        public async Task<ActionResult<Guid>> Create()
        {
            var id = await _cartService.CreateAsync();

            return Created(string.Empty, new { Id = id });
        }

        [HttpPost("{id}/Product/{productId}")]
        [SwaggerOperation(Summary = "Add product to cart")]
        public async Task<ActionResult> AddProductToCart(Guid id, Guid productId, [FromQuery] int quantity)
        {
            await _cartService.AddToCartAsync(id, productId, quantity);

            return NoContent();
        }

        [HttpPut("{id}/Product/{productId}")]
        [SwaggerOperation(Summary = "Update product from cart")]
        public async Task<ActionResult> UpdateProductFromCart(Guid id, Guid productId, [FromQuery] int quantity)
        {
            await _cartService.UpdateCartAsync(id, productId, quantity);

            return NoContent();
        }

        [HttpDelete("{id}/Product/{productId}")]
        [SwaggerOperation(Summary = "Remove product from cart")]
        public async Task<ActionResult> RemoveProductFromCart(Guid id, Guid productId)
        {
            await _cartService.RemoveFromCartAsync(id, productId);
            return Ok();
        }
    }
}
