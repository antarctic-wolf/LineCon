using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LineCon.Migrations
{
    public partial class FixCircularConventionConConfigRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConConfigs_Conventions_ConventionId",
                table: "ConConfigs");

            migrationBuilder.DropIndex(
                name: "IX_ConConfigs_ConventionId",
                table: "ConConfigs");

            migrationBuilder.DropColumn(
                name: "ConventionId",
                table: "ConConfigs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ConventionId",
                table: "ConConfigs",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ConConfigs_ConventionId",
                table: "ConConfigs",
                column: "ConventionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConConfigs_Conventions_ConventionId",
                table: "ConConfigs",
                column: "ConventionId",
                principalTable: "Conventions",
                principalColumn: "ConventionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
