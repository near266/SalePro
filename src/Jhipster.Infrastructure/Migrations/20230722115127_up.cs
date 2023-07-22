using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class up : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeCustomer",
                table: "voucherProductsCustomer",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_orders_ProductId",
                table: "orders",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_products_ProductId",
                table: "orders",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_products_ProductId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_ProductId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "TypeCustomer",
                table: "voucherProductsCustomer");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "orders");
        }
    }
}
