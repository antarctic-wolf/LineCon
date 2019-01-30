using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LineCon.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketWindows",
                columns: table => new
                {
                    TicketWindowId = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    Length = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketWindows", x => x.TicketWindowId);
                });

            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    AttendeeId = table.Column<Guid>(nullable: false),
                    ConfirmationNumber = table.Column<string>(nullable: true),
                    BadgeName = table.Column<string>(nullable: true),
                    TicketWindowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.AttendeeId);
                    table.ForeignKey(
                        name: "FK_Attendees_TicketWindows_TicketWindowId",
                        column: x => x.TicketWindowId,
                        principalTable: "TicketWindows",
                        principalColumn: "TicketWindowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_TicketWindowId",
                table: "Attendees",
                column: "TicketWindowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DropTable(
                name: "TicketWindows");
        }
    }
}
