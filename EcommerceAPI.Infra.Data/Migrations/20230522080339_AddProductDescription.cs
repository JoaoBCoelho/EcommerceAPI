using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcommerceAPI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("74882363-e11b-4c92-b381-aa5268cde6ce"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("96502c69-b32c-4d58-826e-6b32a7be724a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ae1a6658-417d-432f-864c-ceb3ed9d2f47"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b9dab449-25d2-49a1-b068-f0b5c3898085"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("eeab80b0-21ff-4460-a8fe-0ef831eb7c70"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("74882363-e11b-4c92-b381-aa5268cde6ce"), "Eletronics" },
                    { new Guid("96502c69-b32c-4d58-826e-6b32a7be724a"), "Health" },
                    { new Guid("ae1a6658-417d-432f-864c-ceb3ed9d2f47"), "Books" },
                    { new Guid("b9dab449-25d2-49a1-b068-f0b5c3898085"), "Home" },
                    { new Guid("eeab80b0-21ff-4460-a8fe-0ef831eb7c70"), "Clothing" }
                });
        }
    }
}
