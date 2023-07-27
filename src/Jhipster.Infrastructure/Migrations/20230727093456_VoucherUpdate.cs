using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class VoucherUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "vouchers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Image",
                table: "vouchers",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ecosystem",
                table: "affiliates",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "vouchers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "vouchers");

            migrationBuilder.DropColumn(
                name: "Ecosystem",
                table: "affiliates");
        }
    }
}
