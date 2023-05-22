using EcommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceAPI.Infra.Data.EntitiesConfiguration
{
    public class ProductProductConfiguration : IEntityTypeConfiguration<ProductProduct>
    {
        public void Configure(EntityTypeBuilder<ProductProduct> builder)
        {
            builder.HasKey(pp => new { pp.ProductId, pp.RelatedProductId });

            builder.HasOne(pp => pp.RelatedProduct)
                .WithMany()
                .HasForeignKey(pp => pp.RelatedProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}