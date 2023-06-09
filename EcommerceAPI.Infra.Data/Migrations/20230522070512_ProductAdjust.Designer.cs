﻿// <auto-generated />
using System;
using EcommerceAPI.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcommerceAPI.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230522070512_ProductAdjust")]
    partial class ProductAdjust
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EcommerceAPI.Domain.Entities.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCheckedOut")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Entities.CartProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartProducts");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("74882363-e11b-4c92-b381-aa5268cde6ce"),
                            Name = "Eletronics"
                        },
                        new
                        {
                            Id = new Guid("96502c69-b32c-4d58-826e-6b32a7be724a"),
                            Name = "Health"
                        },
                        new
                        {
                            Id = new Guid("ae1a6658-417d-432f-864c-ceb3ed9d2f47"),
                            Name = "Books"
                        },
                        new
                        {
                            Id = new Guid("b9dab449-25d2-49a1-b068-f0b5c3898085"),
                            Name = "Home"
                        },
                        new
                        {
                            Id = new Guid("eeab80b0-21ff-4460-a8fe-0ef831eb7c70"),
                            Name = "Clothing"
                        });
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Popularity")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Entities.ProductProduct", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RelatedProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductId", "RelatedProductId");

                    b.HasIndex("RelatedProductId");

                    b.ToTable("ProductProduct");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Entities.CartProduct", b =>
                {
                    b.HasOne("EcommerceAPI.Domain.Entities.Cart", "Cart")
                        .WithMany("CartProducts")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerceAPI.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Entities.Product", b =>
                {
                    b.HasOne("EcommerceAPI.Domain.Entities.Category", "Category")
                        .WithOne()
                        .HasForeignKey("EcommerceAPI.Domain.Entities.Product", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Entities.ProductProduct", b =>
                {
                    b.HasOne("EcommerceAPI.Domain.Entities.Product", null)
                        .WithMany("RelatedProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerceAPI.Domain.Entities.Product", "RelatedProduct")
                        .WithMany()
                        .HasForeignKey("RelatedProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RelatedProduct");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Entities.Cart", b =>
                {
                    b.Navigation("CartProducts");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Entities.Product", b =>
                {
                    b.Navigation("RelatedProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
