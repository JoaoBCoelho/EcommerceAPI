using EcommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceAPI.Infra.Data.EntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new List<Category> 
            {
                new Category(Guid.NewGuid(), "Eletronics"),
                new Category(Guid.NewGuid(), "Health"),
                new Category(Guid.NewGuid(), "Books"),
                new Category(Guid.NewGuid(), "Home"),
                new Category(Guid.NewGuid(), "Clothing")
            });
        }
    }
}
