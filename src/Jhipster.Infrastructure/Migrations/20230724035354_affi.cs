using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class affi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_products_ProductId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_profileCustomer_CustomerId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_CustomerId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_ProductId",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "CustomserId",
                table: "orders",
                newName: "VoucherId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "orders",
                newName: "SalePerson");

            migrationBuilder.AddColumn<string>(
                name: "TransactionName",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BoughtPerson",
                table: "orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "affiliates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReferrerId = table.Column<Guid>(type: "uuid", nullable: true),
                    BuyerId = table.Column<Guid>(type: "uuid", nullable: true),
                    SalerId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParticipantsId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_affiliates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_OrderId",
                table: "products",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_orders_OrderId",
                table: "products",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_orders_OrderId",
                table: "products");

            migrationBuilder.DropTable(
                name: "affiliates");

            migrationBuilder.DropIndex(
                name: "IX_products_OrderId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "TransactionName",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "BoughtPerson",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "VoucherId",
                table: "orders",
                newName: "CustomserId");

            migrationBuilder.RenameColumn(
                name: "SalePerson",
                table: "orders",
                newName: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_CustomerId",
                table: "orders",
                column: "CustomerId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_orders_profileCustomer_CustomerId",
                table: "orders",
                column: "CustomerId",
                principalTable: "profileCustomer",
                principalColumn: "Id");
        }
    }
}
