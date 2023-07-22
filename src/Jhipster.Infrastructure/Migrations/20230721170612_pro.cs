using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class pro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "products",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryDescription",
                table: "products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CategotyId",
                table: "products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PriceNum",
                table: "products",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "warranty",
                table: "products",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryDescription",
                table: "products");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "products");

            migrationBuilder.DropColumn(
                name: "CategotyId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "PriceNum",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "products");

            migrationBuilder.DropColumn(
                name: "warranty",
                table: "products");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "products",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);
        }
    }
}
