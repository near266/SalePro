using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "profileCustomer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "profileCustomer",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "profileCustomer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "profileCustomer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "profileCustomer",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "profileCustomer",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "memberShip",
                table: "profileCustomer",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "profileCustomer");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "profileCustomer");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "profileCustomer");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "profileCustomer");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "profileCustomer");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "profileCustomer");

            migrationBuilder.DropColumn(
                name: "memberShip",
                table: "profileCustomer");
        }
    }
}
