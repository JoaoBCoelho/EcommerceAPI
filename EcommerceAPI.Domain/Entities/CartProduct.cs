namespace EcommerceAPI.Domain.Entities
{
    public sealed class CartProduct : BaseEntity
    {
        public Guid CartId { get; internal set; }
        public Guid ProductId { get; internal set; }
        public int Quantity { get; internal set; }
        public Product Product { get; set; }
        public Cart Cart { get; set; }

        public CartProduct(Guid cartId, Guid productId, int quantity)
        {
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
