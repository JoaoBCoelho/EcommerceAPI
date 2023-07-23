using EcommerceAPI.Domain.Exceptions;

namespace EcommerceAPI.Domain.Entities
{
    public sealed class Product : BaseEntity
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Popularity { get; private set; }
        public string ImageUrl { get; private set; }
        public string Description { get; private set; }

        public Guid CategoryId { get; private set; }
        public Category Category { get; set; }
        public Guid? ParentProductId { get; private set; }
        public List<ProductProduct> RelatedProducts { get; set; }

        public Product(Guid id)
        {
            this.Id = id;
        }

        public Product(string name,
            decimal price,
            string imageUrl,
            int popularity,
            string description,
            Guid categoryId,
            IEnumerable<Guid>? relatedProducts)
        {
            ValidateProduct(name, price, imageUrl, popularity, categoryId, relatedProducts);

            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            Popularity = popularity;
            CategoryId = categoryId;
            Description = description;
            RelatedProducts = relatedProducts?.Select(id => new ProductProduct(this.Id, id)).ToList();
        }

        public void AddRelatedProduct(Guid relatedProductId)
        {
            RelatedProducts ??= new List<ProductProduct>();

            if (RelatedProducts.Any(a => a.RelatedProductId == relatedProductId))
                throw new DomainValidationException("The informed product already exists.");


            RelatedProducts.Add(new ProductProduct(Id, relatedProductId));
        }

        public void RemoveRelatedProduct(Guid relatedProductId)
        {
            if (RelatedProducts is null)
                throw new DomainValidationException("The product has no related products.");

            var itemToRemove = RelatedProducts.FirstOrDefault(i => i.RelatedProductId == relatedProductId);

            if (itemToRemove is null)
                throw new DomainValidationException("The informed product does not exist.");

            RelatedProducts.Remove(itemToRemove);
        }

        private static void ValidateProduct(string name, decimal price, string imageUrl, int popularity, Guid categoryId, IEnumerable<Guid> relatedProducts)
        {
            if (string.IsNullOrEmpty(name))
                throw new DomainValidationException("Invalid name.");

            if (price <= 0)
                throw new DomainValidationException("Invalid price value.");

            if (popularity < 1 || popularity > 5)
                throw new DomainValidationException("Invalid popularity value.");

            if (categoryId == Guid.Empty)
                throw new DomainValidationException("Invalid category id.");

            if (String.IsNullOrEmpty(imageUrl))
                throw new DomainValidationException("Invalid image url.");

            if (relatedProducts?.Any(id => id == Guid.Empty) == true)
                throw new DomainValidationException("One or more invalid product ids were informed.");
        }
    }
}
