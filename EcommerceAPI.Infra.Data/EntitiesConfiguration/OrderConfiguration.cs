using EcommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceAPI.Infra.Data.EntitiesConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasOne(o => o.BillingInformation)
                .WithOne()
                .HasForeignKey<Order>(o => o.BillingInformationId);

            builder.HasOne(o => o.ShippingInformation)
                .WithOne()
                .HasForeignKey<Order>(o => o.ShippingInformationId);

            builder.HasOne(o => o.Cart)
                .WithOne()
                .HasForeignKey<Order>(o => o.CartId);

            builder.Property(p => p.TotalAmount)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
