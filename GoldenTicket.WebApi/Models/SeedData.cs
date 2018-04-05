using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenTicket.WebApi.Models
{
    public static class SeedData
    {
        private static Technician[] _technicians = {
            new Technician {
                Name = "Madeline Booth",
                IsAdmin = true
            },
            new Technician {
                Name = "Charles Woods",
                IsAdmin = true
            },
            new Technician {
                Name = "Nico Perkins",
                IsAdmin = true
            },
            new Technician {
                Name = "Marie Wilson",
                IsAdmin = false
            },
            new Technician {
                Name = "Nancy Mays",
                IsAdmin = false
            },
            new Technician {
                Name = "Taryn Norman",
                IsAdmin = false
            },
            new Technician {
                Name = "Kieran Lam",
                IsAdmin = false
            },
            new Technician {
                Name = "Natalya Lynch",
                IsAdmin = false
            },
            new Technician {
                Name = "Gavin Preston",
                IsAdmin = false
            },
            new Technician {
                Name = "Kira Paul",
                IsAdmin = false
            },
            new Technician {
                Name = "Shyla Turner",
                IsAdmin = false
            },
            new Technician {
                Name = "Ana Wise",
                IsAdmin = false
            },
            new Technician {
                Name = "Rylan Bryan",
                IsAdmin = false
            },
            new Technician {
                Name = "Cailyn Melton",
                IsAdmin = false
            },
            new Technician {
                Name = "Rory Clark",
                IsAdmin = false
            }
        };

        private static Client[] _clients = {
            new Client {
                Address = "19 Nicolls Court Rego Park, NY 11374",
                Company = "Openlane",
                EmailAddress = "Openlane",
                FirstName = "Elizebeth",
                LastName = "Salgado",
                PhoneNumber = "(738) 531-3531"
            },
            new Client {
                Address = "14 Front St. Winston Salem, NC 27103",
                Company = "Yearin",
                EmailAddress = "Yearin",
                FirstName = "Maddie",
                LastName = "Streater",
                PhoneNumber = "(485) 563-0017"
            },
            new Client {
                Address = "12 Galvin Lane Snohomish, WA 98290",
                Company = "Goodsilron",
                EmailAddress = "Goodsilron",
                FirstName = "Chrissy",
                LastName = "Noffsinger",
                PhoneNumber = "(755) 616-9245"
            },
            new Client {
                Address = "9 Honey Creek Road Victoria, TX 77904",
                Company = "Condax",
                EmailAddress = "Condax",
                FirstName = "Eufemia",
                LastName = "Max",
                PhoneNumber = "(637) 949-5699"
            },
            new Client {
                Address = "791 West Lower River Street Peabody, MA 01960",
                Company = "Opentech",
                EmailAddress = "Opentech",
                FirstName = "Teresa",
                LastName = "Honea",
                PhoneNumber = "(469) 853-6224"
            },
            new Client {
                Address = "67 Blue Spring St. Hillsboro, OR 97124",
                Company = "Golddex",
                EmailAddress = "Golddex",
                FirstName = "Kendrick",
                LastName = "Haydon",
                PhoneNumber = "(695) 261-7236"
            },
            new Client {
                Address = "61 Old Buttonwood Drive Grand Rapids, MI 49503",
                Company = "year-job",
                EmailAddress = "year-job",
                FirstName = "Napoleon",
                LastName = "Bernardo",
                PhoneNumber = "(205) 749-6785"
            },
            new Client {
                Address = "769 Elizabeth St. Greenfield, IN 46140",
                Company = "Isdom",
                EmailAddress = "Isdom",
                FirstName = "Jule",
                LastName = "Rigdon",
                PhoneNumber = "(295) 676-0045"
            },
            new Client {
                Address = "8728 Clay Ave. New City, NY 10956",
                Company = "Gogozoom",
                EmailAddress = "Gogozoom",
                FirstName = "Michaela",
                LastName = "Spady",
                PhoneNumber = "(684) 255-3602"
            },
            new Client {
                Address = "95 Wayne St. Reno, NV 89523",
                Company = "Y-corporation",
                EmailAddress = "Y-corporation",
                FirstName = "Derek",
                LastName = "Raley",
                PhoneNumber = "(366) 249-6137"
            },
            new Client {
                Address = "444 Saxon Court Deland, FL 32720",
                Company = "Nam-zim",
                EmailAddress = "Nam-zim",
                FirstName = "Lindsy",
                LastName = "Messineo",
                PhoneNumber = "(292) 765-7653"
            },
            new Client {
                Address = "9931 Roberts Ave. Columbus, GA 31904",
                Company = "Donquadtech",
                EmailAddress = "Donquadtech",
                FirstName = "Reggie",
                LastName = "Strohm",
                PhoneNumber = "(313) 365-0177"
            },
            new Client {
                Address = "611 Oakwood Rd. Hudson, NH 03051",
                Company = "Warephase",
                EmailAddress = "Warephase",
                FirstName = "Sheilah",
                LastName = "Troia",
                PhoneNumber = "(366) 970-8567"
            },
            new Client {
                Address = "210C Snake Hill Street Ambler, PA 19002",
                Company = "Donware",
                EmailAddress = "Donware",
                FirstName = "Vivien",
                LastName = "Modesto",
                PhoneNumber = "(725) 774-7198"
            },
            new Client {
                Address = "287 Grand St. North Olmsted, OH 44070",
                Company = "Faxquote",
                EmailAddress = "Faxquote",
                FirstName = "Evia",
                LastName = "Days",
                PhoneNumber = "(764) 373-1146"
            }
        };

        public static void Initialize(GoldenTicketContext context)
        {
            context.Technicians.RemoveRange(context.Technicians);
            context.Tickets.RemoveRange(context.Tickets);
            context.Clients.RemoveRange(context.Clients);
            context.TechnicianTicketTimes.RemoveRange(context.TechnicianTicketTimes);
            context.SaveChanges();

            var randGenerator = new Random();

            foreach (var client in _clients)
            {
                client.DateAdded = DateTime.Now.AddMonths(randGenerator.Next(-24, -12));
            }

            foreach (var technician in _technicians)
            {
                technician.DateAdded = DateTime.Now.AddMonths(randGenerator.Next(-36, -25));
            }

            context.Clients.AddRange(_clients);
            context.Technicians.AddRange(_technicians);

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
                        IsUrgent = randGenerator.Next(0, 5) == 0,
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