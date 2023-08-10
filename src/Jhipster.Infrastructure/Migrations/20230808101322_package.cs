using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class package : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "packageMembers");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "packageMembers",
                newName: "StartDate");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "profileCustomer",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "infoPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileMemberId = table.Column<Guid>(type: "uuid", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: true),
                    CurrentStatusMember = table.Column<int>(type: "integer", nullable: true),
                    ProfileCustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    PackageId = table.Column<Guid>(type: "uuid", nullable: true),
                    PackageMemberId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_infoPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_infoPackages_packageMembers_PackageMemberId",
                        column: x => x.PackageMemberId,
                        principalTable: "packageMembers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_infoPackages_profileCustomer_ProfileCustomerId",
                        column: x => x.ProfileCustomerId,
                        principalTable: "profileCustomer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_infoPackages_PackageMemberId",
                table: "infoPackages",
                column: "PackageMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_infoPackages_ProfileCustomerId",
                table: "infoPackages",
                column: "ProfileCustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "infoPackages");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "profileCustomer");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "packageMembers",
                newName: "Created");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "packageMembers",
                type: "integer",
                nullable: true);
        }
    }
}
