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
    public class CategoryController : ApiControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all categories")]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            var categories = await _categoryService.GetAsync();

            return categories is not null && categories.Any()
                ? Ok(categories)
                : NoContent();
        }
    }
}
