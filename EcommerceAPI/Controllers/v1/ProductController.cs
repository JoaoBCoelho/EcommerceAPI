using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.Interfaces;
using EcommerceAPI.Controllers.Shared;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EcommerceAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get product by filter")]
        public async Task<IActionResult> Get([FromQuery] ProductFilterDTO filter)
        {
            var products = await _productService.GetAsync(filter);

            return products is not null && products.Any()
                ? Ok(products)
                : NoContent();
        }


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get by id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _productService.GetAsync(id);

            return product is not null
                ? Ok(product)
                : NoContent();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new product")]
        public async Task<ActionResult<Guid>> CreateProduct(CreateProductDTO productDTO)
        {
            var id = await _productService.CreateAsync(productDTO);

            return Created(string.Empty, new { Id = id });
        }

        [HttpPost("{id}/related/{relatedProductId}")]
        [SwaggerOperation(Summary = "Add related product")]
        public async Task<ActionResult> AddProductToCart(Guid id, Guid productId)
        {
            await _productService.AddRelatedProductAsync(id, productId);

            return NoContent();
        }

        [HttpDelete("{id}/related/{relatedProductId}")]
        [SwaggerOperation(Summary = "Remove related product")]
        public async Task<ActionResult> RemoveRelatedProduct(Guid id, Guid relatedProductId)
        {
            await _productService.RemoveRelatedProductAsync(id, relatedProductId);
            return Ok();
        }
    }
}