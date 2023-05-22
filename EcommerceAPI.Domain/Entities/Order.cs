using EcommerceAPI.Domain.Exceptions;

namespace EcommerceAPI.Domain.Entities
{
    public sealed class Order : BaseEntity
    {
        public string CustomerEmail { get; private set; }
        public decimal TotalAmount { get; private set; }

        public Guid CartId { get; set; }
        public Cart Cart { get; private set; }

        public Guid BillingInformationId { get; private set; }
        public BillingInformation BillingInformation { get; private set; }

        public Guid ShippingInformationId { get; private set; }
        public ShippingInformation ShippingInformation { get; private set; }

        public Order()
        {
            // Parameterless constructor for Entity Framework
        }

        public Order(BillingInformation billingInformation, ShippingInformation shippingInformation, string customerEmail, Cart cart)
        {
            ValidateOrder(billingInformation, shippingInformation, cart, customerEmail);

            BillingInformation = billingInformation;
            ShippingInformation = shippingInformation;
            CustomerEmail = customerEmail;
            CartId = cart.Id;
            TotalAmount = cart.CartProducts.Select(s => s.Product?.Price * s.Quantity ?? 0).Sum();
        }

        private static void ValidateOrder(BillingInformation billingInformation, ShippingInformation shippingInformation, Cart cart, string customerEmail)
        {
            if (cart is null)
                throw new NotFoundException("Invalid cart.");

            if (cart?.CartProducts?.Any() != true)
                throw new NotFoundException("There are no products in the cart.");

            if (cart.IsCheckedOut)
                throw new DomainValidationException("The cart was already checked out.");

            if (billingInformation is null)
                throw new DomainValidationException("Invalid billing information.");

            if (shippingInformation is null)
                throw new DomainValidationException("Invalid shipping information.");

            if (string.IsNullOrEmpty(customerEmail))
                throw new DomainValidationException("Invalid customer email.");
        }
    }
}
