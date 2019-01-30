using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LineCon.Migrations
{
    public partial class AttendeeTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_TicketWindows_TicketWindowId",
                table: "Attendees");

            migrationBuilder.DropIndex(
                name: "IX_Attendees_TicketWindowId",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "TicketWindowId",
                table: "Attendees");

            migrationBuilder.AddColumn<int>(
                name: "AttendeeCapacity",
                table: "TicketWindows",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmationNumber",
                table: "Attendees",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BadgeName",
                table: "Attendees",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AttendeeTickets",
                columns: table => new
                {
                    AttendeeTicketId = table.Column<Guid>(nullable: false),
                    Completed = table.Column<bool>(nullable: false),
                    AttendeeId = table.Column<Guid>(nullable: false),
                    TicketWindowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendeeTickets", x => x.AttendeeTicketId);
                    table.ForeignKey(
                        name: "FK_AttendeeTickets_Attendees_AttendeeId",
                        column: x => x.AttendeeId,
                        principalTable: "Attendees",
                        principalColumn: "AttendeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendeeTickets_TicketWindows_TicketWindowId",
                        column: x => x.TicketWindowId,
                        principalTable: "TicketWindows",
                        principalColumn: "TicketWindowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendeeTickets_AttendeeId",
                table: "AttendeeTickets",
                column: "AttendeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendeeTickets_TicketWindowId",
                table: "AttendeeTickets",
                column: "TicketWindowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendeeTickets");

            migrationBuilder.DropColumn(
                name: "AttendeeCapacity",
                table: "TicketWindows");

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmationNumber",
                table: "Attendees",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "BadgeName",
                table: "Attendees",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "TicketWindowId",
                table: "Attendees",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_TicketWindowId",
                table: "Attendees",
                column: "TicketWindowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_TicketWindows_TicketWindowId",
                table: "Attendees",
                column: "TicketWindowId",
                principalTable: "TicketWindows",
                principalColumn: "TicketWindowId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
