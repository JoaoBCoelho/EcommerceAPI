using EcommerceAPI.Domain.Exceptions;

namespace EcommerceAPI.Domain.Entities
{
    public sealed class Order : BaseEntity
    {
        public string BillingAddress { get; private set; }
        public string ShippingAddress { get; private set; }
        public string CustomerEmail { get; private set; }
        public decimal TotalAmount { get; private set; }

        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        public Order(string billingAddress, string shippingAddress, string email, Cart cart)
        {
            ValidateOrder(billingAddress, shippingAddress, email);

            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
            CustomerEmail = email;
            CartId = cart.Id;
            TotalAmount = cart.CartProducts.Select(s => s.Product.Price * s.Quantity).Sum();
        }

        private static void ValidateOrder(string billingAddress, string shippingAddress, string email)
        {
            if (string.IsNullOrEmpty(billingAddress))
                throw new DomainValidationException("Invalid billing address.");

            if (string.IsNullOrEmpty(shippingAddress))
                throw new DomainValidationException("Invalid shipping address.");

            if (string.IsNullOrEmpty(email))
                throw new DomainValidationException("Invalid email.");
        }
    }
}
