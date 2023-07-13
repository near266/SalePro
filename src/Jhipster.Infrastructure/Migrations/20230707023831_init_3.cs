using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class init_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventJoins_UserProfiles_UserProfilesId",
                table: "EventJoins");

            migrationBuilder.DropIndex(
                name: "IX_EventJoins_UserProfilesId",
                table: "EventJoins");

            migrationBuilder.RenameColumn(
                name: "UserProfilesId",
                table: "EventJoins",
                newName: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_EventJoins_UserId",
                table: "EventJoins",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventJoins_UserProfiles_UserId",
                table: "EventJoins",
                column: "UserId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventJoins_UserProfiles_UserId",
                table: "EventJoins");

            migrationBuilder.DropIndex(
                name: "IX_EventJoins_UserId",
                table: "EventJoins");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "EventJoins",
                newName: "UserProfilesId");

            migrationBuilder.CreateIndex(
                name: "IX_EventJoins_UserProfilesId",
                table: "EventJoins",
                column: "UserProfilesId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventJoins_UserProfiles_UserProfilesId",
                table: "EventJoins",
                column: "UserProfilesId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }
    }
}
