namespace EcommerceAPI.Application.DTOs
{
    public class ProductFilterDTO
    {
        public string? Name { get; set; }
        public Guid? Category { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public int? PopularityMin { get; set; }
        public int? PopularityMax { get; set; }
    }
}
