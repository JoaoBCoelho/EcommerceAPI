using EcommerceAPI.Domain.Exceptions;

namespace EcommerceAPI.Domain.Entities
{
    public sealed class Cart : BaseEntity
    {
        public List<CartProduct> CartProducts { get; private set; }
        public bool IsCheckedOut { get; private set; }

        public void CheckOut()
        {
            if (IsCheckedOut)
            {
                throw new DomainValidationException("The informed cart was already checked out.");
            }

            IsCheckedOut = true;
        }

        public void AddProduct(Guid productId, int quantity)
        {
            ValidateCheckout();
            ValidateQuantity(quantity);

            if (CartProducts.Any(a => a.ProductId == productId))
                throw new DomainValidationException("The informed product is already in the cart.");

            CartProducts.Add(new CartProduct(Id, productId, quantity));
        }

        public void UpdateProductQuantity(Guid productId, int quantity)
        {
            ValidateCheckout();
            ValidateQuantity(quantity);

            var itemToUpdate = CartProducts.FirstOrDefault(i => i.ProductId == productId);

            if (itemToUpdate is null)
                throw new DomainValidationException("The informed product is not in the cart.");

            itemToUpdate.Quantity = quantity;
        }

        public void RemoveProduct(Guid productId)
        {
            ValidateCheckout();

            var itemToRemove = CartProducts.FirstOrDefault(i => i.ProductId == productId);

            if (itemToRemove is null)
                throw new DomainValidationException("The informed product is not in the cart.");

            CartProducts.Remove(itemToRemove);
        }

        private void ValidateCheckout()
        {
            if (IsCheckedOut)
                throw new DomainValidationException("The cart was already checked out.");
        }

        private static void ValidateQuantity(int quantity)
        {
            if (quantity < 1)
                throw new DomainValidationException("Quantity must be greater than zero.");
        }
    }
}
