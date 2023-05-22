using AutoMapper;
using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Domain.Entities;

namespace EcommerceAPI.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductDTO, Product>()
                .ConstructUsing(src => new Product(
                    src.Name,
                    src.Price,
                    src.ImageUrl,
                    new Random().Next(1, 5),
                    src.Description,
                    src.Category,
                    src.RelatedProducts))
                .ForMember(f => f.Category, opt => opt.Ignore())
                .ForMember(f => f.RelatedProducts, opt => opt.Ignore());

            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>()
                .ForMember(f => f.RelatedProducts, opt => opt.MapFrom(f => f.RelatedProducts.Select(s => s.RelatedProduct)));

            CreateMap<Cart, CartDTO>()
                .ForMember(f => f.Products, opt => opt.MapFrom(f => f.CartProducts));

            CreateMap<CartProduct, CartProductDTO>()
                .ForMember(f => f.Quantity, opt => opt.MapFrom(f => f.Quantity))
                .ForMember(f => f.Product, opt => opt.MapFrom(f => f.Product));

            CreateMap<CartProduct, ProductDTO>()
                .ForMember(f => f.Id, opt => opt.MapFrom(f => f.Product.Id))
                .ForMember(f => f.Name, opt => opt.MapFrom(f => f.Product.Name))
                .ForMember(f => f.Price, opt => opt.MapFrom(f => f.Product.Price))
                .ForMember(f => f.Popularity, opt => opt.MapFrom(f => f.Product.Popularity))
                .ForMember(f => f.Description, opt => opt.MapFrom(f => f.Product.Description))
                .ForMember(f => f.ImageUrl, opt => opt.MapFrom(f => f.Product.ImageUrl))
                .ForMember(f => f.Category, opt => opt.MapFrom(f => f.Product.Category))
                .ForMember(f => f.RelatedProducts, opt => opt.MapFrom(f => f.Product.RelatedProducts));
        }
    }
}
