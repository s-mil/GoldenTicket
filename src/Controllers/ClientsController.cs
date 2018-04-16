using System;
using System.Linq;
using System.Threading.Tasks;
using GoldenTicket.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoldenTicket.Models.ClientsViewModels;

namespace GoldenTicket.Controllers
{
    [Authorize]
    public partial class ClientsController : Controller
    {
        private GoldenTicketContext _context;

        public ClientsController(GoldenTicketContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var clients = await _context.Clients.GroupJoin(_context.Tickets.Where(ticket => ticket.Open), client => client.Id, ticket => ticket.ClientId, (client, tickets) => new ClientDetails { Client = client, Tickets = tickets.ToList() }).ToListAsync();
            return View(clients);
        }
    }
}