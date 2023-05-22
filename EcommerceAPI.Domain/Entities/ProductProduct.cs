namespace EcommerceAPI.Domain.Entities
{
    public class ProductProduct
    {
        public Guid ProductId { get; set; }

        public Guid RelatedProductId { get; set; }

        public Product RelatedProduct { get; set; }

        public ProductProduct(Guid productId, Guid relatedProductId)
        {
            ProductId = productId;
            RelatedProductId = relatedProductId;
        }
    }
}
