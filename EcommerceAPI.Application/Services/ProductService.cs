using AutoMapper;
using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.Interfaces;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Exceptions;
using EcommerceAPI.Domain.Interfaces;

namespace EcommerceAPI.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,
                ICategoryRepository categoryRepository,
                IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task AddRelatedProductAsync(Guid id, Guid relatedProductId)
        {
            var product = await _productRepository.GetAsync(id);
            if (product is null)
            {
                throw new NotFoundException("The product was not found.");
            }

            var relatedProduct = await _productRepository.GetAsync(relatedProductId);
            if (relatedProduct is null)
            {
                throw new NotFoundException("The related product was not found.");
            }

            product.AddRelatedProduct(relatedProductId);
            await _productRepository.UpdateAsync(product);
        }

        public async Task<Guid> CreateAsync(CreateProductDTO productDTO)
        {
            await ValidateNewProductInnerEntitiesExistenceAsync(productDTO);
            Product product = _mapper.Map<Product>(productDTO);
            await _productRepository.CreateAsync(product);

            return product.Id;
        }

        public async Task<ProductDTO> GetAsync(Guid productId)
        {
            Product product = await _productRepository.GetAsync(productId);
            var productDto = _mapper.Map<ProductDTO>(product);

            return productDto;
        }

        public async Task<IEnumerable<ProductDTO>> GetAsync(ProductFilterDTO filterDto)
        {
            var products = await _productRepository.GetAsync();

            if (!string.IsNullOrEmpty(filterDto.Name))
            {
                products = products.Where(p => p.Name.ToLower().Contains(filterDto.Name.ToLower()));
            }

            if (filterDto.Category.HasValue)
            {
                products = products.Where(p => p.CategoryId == filterDto.Category);
            }

            if (filterDto.PriceMin.HasValue)
            {
                products = products.Where(p => p.Price >= filterDto.PriceMin);
            }

            if (filterDto.PriceMax.HasValue)
            {
                products = products.Where(p => p.Price <= filterDto.PriceMax);
            }

            if (filterDto.PopularityMin.HasValue)
            {
                products = products.Where(p => p.Popularity >= filterDto.PopularityMin);
            }

            if (filterDto.PopularityMax.HasValue)
            {
                products = products.Where(p => p.Popularity <= filterDto.PopularityMax);
            }

            var productDtos = _mapper.Map<List<ProductDTO>>(products);

            return productDtos;
        }

        public async Task RemoveRelatedProductAsync(Guid id, Guid relatedProductId)
        {
            var product = await _productRepository.GetAsync(id);
            if (product is null)
            {
                throw new NotFoundException("The product was not found.");
            }

            var relatedProduct = await _productRepository.GetAsync(relatedProductId);
            if (relatedProduct is null)
            {
                throw new NotFoundException("The related product was not found.");
            }

            product.RemoveRelatedProduct(relatedProductId);
            await _productRepository.UpdateAsync(product);
        }

        private async Task ValidateNewProductInnerEntitiesExistenceAsync(CreateProductDTO productDTO)
        {
            if (productDTO.RelatedProducts?.Any() == true)
            {
                var relatedProductsExist = await _productRepository.AllExistAsync(productDTO.RelatedProducts);
                if (!relatedProductsExist)
                {
                    throw new NotFoundException("One or more related products does not exist.");
                }
            }

            var category = await _categoryRepository.GetAsync(productDTO.Category);
            if (category is null)
            {
                throw new NotFoundException("The informed category does not exist.");
            }
        }
    }
}
