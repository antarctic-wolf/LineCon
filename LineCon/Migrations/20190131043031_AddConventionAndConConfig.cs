using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LineCon.Migrations
{
    public partial class AddConventionAndConConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ConventionId",
                table: "TicketWindows",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ConventionId",
                table: "AttendeeTickets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ConventionId",
                table: "Attendees",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Conventions",
                columns: table => new
                {
                    ConventionId = table.Column<Guid>(nullable: false),
                    ConConfigId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conventions", x => x.ConventionId);
                });

            migrationBuilder.CreateTable(
                name: "ConConfigs",
                columns: table => new
                {
                    ConConfigId = table.Column<Guid>(nullable: false),
                    ConventionId = table.Column<Guid>(nullable: false),
                    TicketWindowInterval = table.Column<TimeSpan>(nullable: false),
                    TicketWindowCapacity = table.Column<int>(nullable: false),
                    RequireConfirmationNumber = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConConfigs", x => x.ConConfigId);
                    table.ForeignKey(
                        name: "FK_ConConfigs_Conventions_ConventionId",
                        column: x => x.ConventionId,
                        principalTable: "Conventions",
                        principalColumn: "ConventionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfirmationNumbers",
                columns: table => new
                {
                    ConConfigId = table.Column<Guid>(nullable: false),
                    ConventionId = table.Column<Guid>(nullable: false),
                    Number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmationNumbers", x => x.ConConfigId);
                    table.ForeignKey(
                        name: "FK_ConfirmationNumbers_Conventions_ConventionId",
                        column: x => x.ConventionId,
                        principalTable: "Conventions",
                        principalColumn: "ConventionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationHours",
                columns: table => new
                {
                    RegistrationHoursId = table.Column<Guid>(nullable: false),
                    ConConfigId = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationHours", x => x.RegistrationHoursId);
                    table.ForeignKey(
                        name: "FK_RegistrationHours_ConConfigs_ConConfigId",
                        column: x => x.ConConfigId,
                        principalTable: "ConConfigs",
                        principalColumn: "ConConfigId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketWindows_ConventionId",
                table: "TicketWindows",
                column: "ConventionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendeeTickets_ConventionId",
                table: "AttendeeTickets",
                column: "ConventionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_ConventionId",
                table: "Attendees",
                column: "ConventionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConConfigs_ConventionId",
                table: "ConConfigs",
                column: "ConventionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmationNumbers_ConventionId",
                table: "ConfirmationNumbers",
                column: "ConventionId");

            migrationBuilder.CreateIndex(
                name: "IX_Conventions_ConConfigId",
                table: "Conventions",
                column: "ConConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationHours_ConConfigId",
                table: "RegistrationHours",
                column: "ConConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_Conventions_ConventionId",
                table: "Attendees",
                column: "ConventionId",
                principalTable: "Conventions",
                principalColumn: "ConventionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendeeTickets_Conventions_ConventionId",
                table: "AttendeeTickets",
                column: "ConventionId",
                principalTable: "Conventions",
                principalColumn: "ConventionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketWindows_Conventions_ConventionId",
                table: "TicketWindows",
                column: "ConventionId",
                principalTable: "Conventions",
                principalColumn: "ConventionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conventions_ConConfigs_ConConfigId",
                table: "Conventions",
                column: "ConConfigId",
                principalTable: "ConConfigs",
                principalColumn: "ConConfigId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_Conventions_ConventionId",
                table: "Attendees");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendeeTickets_Conventions_ConventionId",
                table: "AttendeeTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketWindows_Conventions_ConventionId",
                table: "TicketWindows");

            migrationBuilder.DropForeignKey(
                name: "FK_ConConfigs_Conventions_ConventionId",
                table: "ConConfigs");

            migrationBuilder.DropTable(
                name: "ConfirmationNumbers");

            migrationBuilder.DropTable(
                name: "RegistrationHours");

            migrationBuilder.DropTable(
                name: "Conventions");

            migrationBuilder.DropTable(
                name: "ConConfigs");

            migrationBuilder.DropIndex(
                name: "IX_TicketWindows_ConventionId",
                table: "TicketWindows");

            migrationBuilder.DropIndex(
                name: "IX_AttendeeTickets_ConventionId",
                table: "AttendeeTickets");

            migrationBuilder.DropIndex(
                name: "IX_Attendees_ConventionId",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "ConventionId",
                table: "TicketWindows");

            migrationBuilder.DropColumn(
                name: "ConventionId",
                table: "AttendeeTickets");

            migrationBuilder.DropColumn(
                name: "ConventionId",
                table: "Attendees");
        }
    }
}
