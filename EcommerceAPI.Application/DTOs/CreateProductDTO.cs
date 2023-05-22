using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Application.DTOs
{
    public class CreateProductDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public Guid Category { get; set; }
        public IEnumerable<Guid>? RelatedProducts { get; set; }
    }
}
