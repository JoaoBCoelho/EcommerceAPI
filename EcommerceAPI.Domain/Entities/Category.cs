namespace EcommerceAPI.Domain.Entities
{
    public sealed class Category : BaseEntity
    {
        public string Name { get; private set; }

        public Category(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}