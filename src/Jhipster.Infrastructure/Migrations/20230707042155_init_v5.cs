using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class init_v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckInLocation",
                table: "EventJoins",
                newName: "CheckedInLocation");

            migrationBuilder.RenameColumn(
                name: "CheckInDate",
                table: "EventJoins",
                newName: "CheckedInDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckedInLocation",
                table: "EventJoins",
                newName: "CheckInLocation");

            migrationBuilder.RenameColumn(
                name: "CheckedInDate",
                table: "EventJoins",
                newName: "CheckInDate");
        }
    }
}
