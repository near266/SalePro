using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class ordertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AffiliateId",
                table: "orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AffiliatesId",
                table: "orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_AffiliatesId",
                table: "orders",
                column: "AffiliatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_affiliates_AffiliatesId",
                table: "orders",
                column: "AffiliatesId",
                principalTable: "affiliates",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_affiliates_AffiliatesId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_AffiliatesId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "AffiliateId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "AffiliatesId",
                table: "orders");
        }
    }
}
