using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace GoldenTicket.WebApi.Models
{
    public static class SeedData
    {
        public static void Initialize(GoldenTicketContext context)
        {
            // Look for any movies.
            if (context.Tickets.Any())
            {
                return;   // DB has been seeded
            }
            var randGenerator = new Random();
            for (var i = 0; i < 15; i++) {
                context.Tickets.Add(new Ticket {
                    ClientId = Guid.NewGuid(),
                    Title = $"Ticket {i}",
                    Description = $"Super awesome ticket {i}",
                    Complexity = i % 3,
                    IsUrgent = randGenerator.NextDouble() > 0.5,
                    Notes = "Terrible client to work with"
                });
            }
            context.SaveChanges();
        }
    }
}