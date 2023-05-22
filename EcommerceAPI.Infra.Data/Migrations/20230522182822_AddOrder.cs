using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcommerceAPI.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "BillingInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingInformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingInformations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillingInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_BillingInformations_BillingInformationId",
                        column: x => x.BillingInformationId,
                        principalTable: "BillingInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_ShippingInformations_ShippingInformationId",
                        column: x => x.ShippingInformationId,
                        principalTable: "ShippingInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1dcd6233-bd60-4ccb-95c1-20eca9e2feee"), "Eletronics" },
                    { new Guid("27c4224a-1018-4f5b-a872-be7bc68468ce"), "Books" },
                    { new Guid("36d5242e-d50a-4460-adf1-d77cc3d0859f"), "Home" },
                    { new Guid("3bea9260-eae8-40f5-bd11-a740c51d34ea"), "Health" },
                    { new Guid("c955a6f9-8a4b-4197-8cdb-2452ca15ddea"), "Clothing" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BillingInformationId",
                table: "Orders",
                column: "BillingInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CartId",
                table: "Orders",
                column: "CartId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingInformationId",
                table: "Orders",
                column: "ShippingInformationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "BillingInformations");

            migrationBuilder.DropTable(
                name: "ShippingInformations");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1dcd6233-bd60-4ccb-95c1-20eca9e2feee"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("27c4224a-1018-4f5b-a872-be7bc68468ce"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("36d5242e-d50a-4460-adf1-d77cc3d0859f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3bea9260-eae8-40f5-bd11-a740c51d34ea"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c955a6f9-8a4b-4197-8cdb-2452ca15ddea"));

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
        }
    }
}
