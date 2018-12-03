using Microsoft.EntityFrameworkCore.Migrations;

namespace LineCon.Migrations
{
    public partial class TicketWindow_dbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_TicketWindow_TicketWindowId",
                table: "Attendees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketWindow",
                table: "TicketWindow");

            migrationBuilder.RenameTable(
                name: "TicketWindow",
                newName: "TicketWindows");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketWindows",
                table: "TicketWindows",
                column: "TicketWindowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_TicketWindows_TicketWindowId",
                table: "Attendees",
                column: "TicketWindowId",
                principalTable: "TicketWindows",
                principalColumn: "TicketWindowId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_TicketWindows_TicketWindowId",
                table: "Attendees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketWindows",
                table: "TicketWindows");

            migrationBuilder.RenameTable(
                name: "TicketWindows",
                newName: "TicketWindow");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketWindow",
                table: "TicketWindow",
                column: "TicketWindowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_TicketWindow_TicketWindowId",
                table: "Attendees",
                column: "TicketWindowId",
                principalTable: "TicketWindow",
                principalColumn: "TicketWindowId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
