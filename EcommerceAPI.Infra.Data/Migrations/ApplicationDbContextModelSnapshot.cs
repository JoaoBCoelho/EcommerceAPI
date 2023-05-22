﻿// <auto-generated />
using System;
using EcommerceAPI.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcommerceAPI.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                            Id = new Guid("f20bf77f-93f2-4b68-aabe-3380cb94a230"),
                            Name = "Eletronics"
                        },
                        new
                        {
                            Id = new Guid("097d362d-b7b1-467b-8340-568aa15c0770"),
                            Name = "Health"
                        },
                        new
                        {
                            Id = new Guid("b3e4e18e-af76-4dc2-8398-70242f22263a"),
                            Name = "Books"
                        },
                        new
                        {
                            Id = new Guid("ebf7f117-a2a0-4699-9ceb-5e07364213b9"),
                            Name = "Home"
                        },
                        new
                        {
                            Id = new Guid("80e55dd4-a5ab-417d-8d68-2fda4310fddf"),
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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.HasIndex("CategoryId");

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
                        .WithMany()
                        .HasForeignKey("CategoryId")
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
