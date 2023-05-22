using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcommerceAPI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdjustedProductCategoryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("234bb0c7-ef39-49ef-a3cb-9e172675e5fb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5d4b6aae-b406-446c-9a09-c2625f123a6e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b88626e6-9674-441f-a7be-349a5dd9cda5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d7b45a0f-c9bf-4312-83dd-c95b672d4a0e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("dc86fc6d-0c9d-456d-a6ae-c559f7fb4678"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("097d362d-b7b1-467b-8340-568aa15c0770"), "Health" },
                    { new Guid("80e55dd4-a5ab-417d-8d68-2fda4310fddf"), "Clothing" },
                    { new Guid("b3e4e18e-af76-4dc2-8398-70242f22263a"), "Books" },
                    { new Guid("ebf7f117-a2a0-4699-9ceb-5e07364213b9"), "Home" },
                    { new Guid("f20bf77f-93f2-4b68-aabe-3380cb94a230"), "Eletronics" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("097d362d-b7b1-467b-8340-568aa15c0770"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("80e55dd4-a5ab-417d-8d68-2fda4310fddf"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b3e4e18e-af76-4dc2-8398-70242f22263a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ebf7f117-a2a0-4699-9ceb-5e07364213b9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f20bf77f-93f2-4b68-aabe-3380cb94a230"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("234bb0c7-ef39-49ef-a3cb-9e172675e5fb"), "Health" },
                    { new Guid("5d4b6aae-b406-446c-9a09-c2625f123a6e"), "Home" },
                    { new Guid("b88626e6-9674-441f-a7be-349a5dd9cda5"), "Books" },
                    { new Guid("d7b45a0f-c9bf-4312-83dd-c95b672d4a0e"), "Clothing" },
                    { new Guid("dc86fc6d-0c9d-456d-a6ae-c559f7fb4678"), "Eletronics" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId",
                unique: true);
        }
    }
}
