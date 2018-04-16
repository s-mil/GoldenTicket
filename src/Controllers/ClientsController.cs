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
    /// <summary>
    /// Controller for Clients
    /// </summary>
    [Authorize]
    public partial class ClientsController : Controller
    {
        private GoldenTicketContext _context;

        /// <summary>
        /// Initializes _context
        /// </summary>
        /// <param name="context">context of client</param>
        public ClientsController(GoldenTicketContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Shows all clients
        /// </summary>
        /// <returns>clients page</returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var clients = await _context.Clients.GroupJoin(_context.Tickets.Where(ticket => ticket.Open), client => client.Id, ticket => ticket.ClientId, (client, tickets) => new ClientDetails { Client = client, Tickets = tickets.ToList() }).ToListAsync();
            return View(clients);
        }
    }
}