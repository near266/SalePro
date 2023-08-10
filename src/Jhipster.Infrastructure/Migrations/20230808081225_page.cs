using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class page : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_packageMembers_profileCustomer_CustomerId",
                table: "packageMembers");

            migrationBuilder.DropIndex(
                name: "IX_packageMembers_CustomerId",
                table: "packageMembers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "packageMembers");

            migrationBuilder.DropColumn(
                name: "Decripstion",
                table: "packageMembers");

            migrationBuilder.DropColumn(
                name: "PackageName",
                table: "packageMembers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "packageMembers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Decripstion",
                table: "packageMembers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PackageName",
                table: "packageMembers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_packageMembers_CustomerId",
                table: "packageMembers",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_packageMembers_profileCustomer_CustomerId",
                table: "packageMembers",
                column: "CustomerId",
                principalTable: "profileCustomer",
                principalColumn: "Id");
        }
    }
}
