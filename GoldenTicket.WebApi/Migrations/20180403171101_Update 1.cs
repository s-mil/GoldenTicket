using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GoldenTicket.WebApi.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicianTickets");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Tickets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Open",
                table: "Tickets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TechnicianTicketTimes",
                columns: table => new
                {
                    TechnicianId = table.Column<Guid>(nullable: false),
                    TicketId = table.Column<Guid>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianTicketTimes", x => new { x.TechnicianId, x.TicketId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicianTicketTimes");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Open",
                table: "Tickets");

            migrationBuilder.CreateTable(
                name: "TechnicianTickets",
                columns: table => new
                {
                    TechnicianId = table.Column<Guid>(nullable: false),
                    TicketId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianTickets", x => new { x.TechnicianId, x.TicketId });
                });
        }
    }
}
