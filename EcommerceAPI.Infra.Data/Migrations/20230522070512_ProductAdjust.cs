using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcommerceAPI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductAdjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4788b02b-b373-4e05-98f2-a410e5ff863e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4aa296c0-0946-463d-a1cb-c12f796b8c9d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9c518b67-880a-495a-af29-932d8eb7e883"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a755b554-136a-4c1c-8ffd-aca0d4674bd6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f26304b7-ced8-483f-b7ce-f61f9f7f9611"));

            migrationBuilder.AddColumn<Guid>(
                name: "ParentProductId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductProduct",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelatedProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProduct", x => new { x.ProductId, x.RelatedProductId });
                    table.ForeignKey(
                        name: "FK_ProductProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProduct_Products_RelatedProductId",
                        column: x => x.RelatedProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductProduct_RelatedProductId",
                table: "ProductProduct",
                column: "RelatedProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductProduct");

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

            migrationBuilder.DropColumn(
                name: "ParentProductId",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4788b02b-b373-4e05-98f2-a410e5ff863e"), "Home" },
                    { new Guid("4aa296c0-0946-463d-a1cb-c12f796b8c9d"), "Health" },
                    { new Guid("9c518b67-880a-495a-af29-932d8eb7e883"), "Clothing" },
                    { new Guid("a755b554-136a-4c1c-8ffd-aca0d4674bd6"), "Eletronics" },
                    { new Guid("f26304b7-ced8-483f-b7ce-f61f9f7f9611"), "Books" }
                });
        }
    }
}
