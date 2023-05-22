namespace EcommerceAPI.Application.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Popularity { get; set; }
        public string ImageUrl { get; set; }
        public CategoryDTO Category { get; set; }
        public List<ProductDTO> RelatedProducts { get; set; }
    }
}
