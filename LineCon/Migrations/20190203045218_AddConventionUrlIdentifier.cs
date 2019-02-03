using Microsoft.EntityFrameworkCore.Migrations;

namespace LineCon.Migrations
{
    public partial class AddConventionUrlIdentifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlIdentifier",
                table: "Conventions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlIdentifier",
                table: "Conventions");
        }
    }
}
