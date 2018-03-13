using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GoldenTicket.WebApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Technicians",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicians", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Complexity = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsUrgent = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Technicians");

            migrationBuilder.DropTable(
                name: "TechnicianTickets");

            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
