using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class init_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "EventJoins",
                newName: "EventTitle");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "EventJoins",
                newName: "CheckInLocation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventTitle",
                table: "EventJoins",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "CheckInLocation",
                table: "EventJoins",
                newName: "Location");
        }
    }
}
