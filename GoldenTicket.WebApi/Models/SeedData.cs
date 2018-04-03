using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenTicket.WebApi.Models
{
    public static class SeedData
    {
        private static List<string> TechnicianNames = new List<string> 
        {
            "Madeline Booth",
            "Charles Woods",
            "Nico Perkins",
            "Marie Wilson",
            "Nancy Mays",
            "Taryn Norman",
            "Kieran Lam",
            "Natalya Lynch",
            "Gavin Preston",
            "Kira Paul",
            "Shyla Turner",
            "Ana Wise",
            "Rylan Bryan",
            "Cailyn Melton",
            "Rory Clark"
        };

        private static List<(string company, string firstName, string LastName)> Clients = new List<(string company, string firstName, string LastName)>
        {
            ("","",""),
            ("","",""),
            ("","",""),
            ("","",""),
            ("","",""),
            ("","",""),
            ("","",""),
            ("","",""),
            ("","",""),
            ("","",""),
            ("","",""),
        };


        public static void Initialize(GoldenTicketContext context)
        {
            context.Technicians.RemoveRange(context.Technicians);
            context.Tickets.RemoveRange(context.Tickets);
            context.Clients.RemoveRange(context.Clients);
            context.TechnicianTicketTimes.RemoveRange(context.TechnicianTicketTimes);
            context.SaveChanges();

            var randGenerator = new Random();

            for (var i = 0; i < 10; i++)
            {
                context.Clients.Add(new Client
                {
                    Address = "101 Drewberrymore Lane, Baton Rouge, LA 70808",
                    Company = "Contoso Inc.",
                    DateAdded = DateTime.Now.AddMonths(randGenerator.Next(-12, 0)),
                    EmailAddress = "accountspayable@contoso.com",
                    FirstName = "Al",
                    LastName = "Ward",
                    PhoneNumber = "2258675309"
                });
            }

            for (var i = 0; i < 15; i++)
            {
                context.Technicians.Add(new Technician
                {
                    DateAdded = DateTime.Now.AddMonths(randGenerator.Next(-24, -12)),
                    Id = new Guid(),
                    IsAdmin = randGenerator.Next(0, 5) == 0,
                    Name = "John Apple"
                });
            }

            context.SaveChanges();

            foreach (var client in context.Clients)
            {
                var ticketCount = randGenerator.Next(0, 7);
                for (var i = 0; i < ticketCount; i++)
                {
                    context.Tickets.Add(new Ticket
                    {
                        ClientId = client.Id,
                        Title = $"{client.Company}: Case {i}",
                        Description = $"Super awesome ticket {i}",
                        Complexity = i % 3,
                        IsUrgent = randGenerator.Next(0, 1) == 0,
                        Notes = "Terrible client to work with"
                    });
                }
            }

            foreach (var ticket in context.Tickets)
            {
                var workTimesCount = randGenerator.Next(0, 10);
                for (var i = 0; i < workTimesCount; i++)
                {
                    var start = ticket.DateAdded.AddHours(randGenerator.Next(1, 60));
                    var end = start.AddHours(randGenerator.Next(1, 5));
                    context.TechnicianTicketTimes.Add(new TechnicianTicketTime
                    {
                        Start = start,
                        End = end,
                        TechnicianId = context.Technicians.OrderBy(t => new Guid()).Take(1).First().Id,
                        TicketId = ticket.Id
                    });
                }
            }

            context.SaveChanges();
        }
    }
}