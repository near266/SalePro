using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jhipster.Infrastructure.Migrations
{
    public partial class delFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_profileCustomer_companies_CompanyId",
                table: "profileCustomer");

            migrationBuilder.DropIndex(
                name: "IX_profileCustomer_CompanyId",
                table: "profileCustomer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_profileCustomer_CompanyId",
                table: "profileCustomer",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_profileCustomer_companies_CompanyId",
                table: "profileCustomer",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id");
        }
    }
}
