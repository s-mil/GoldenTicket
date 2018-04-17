using System;
using System.Linq;
using System.Threading.Tasks;
using GoldenTicket.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoldenTicket.Models.ClientsViewModels;
using GoldenTicket.Models;

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
            var clients = await _context.Clients.GroupJoin(_context.Tickets.Where(ticket => ticket.Open), client => client.Id, ticket => ticket.ClientId, (client, tickets) => new ClientDetails { Client = client, Tickets = tickets, OpenTicketCount = tickets.Count() }).OrderByDescending(details => details.Tickets.Count()).ToListAsync();
            return View(clients);
        }

        /// <summary>
        /// Gets view for adding a ticket.
        /// </summary>
        /// <returns>The view.</returns>
        [HttpGet]
        public async Task<IActionResult> AddTicket([FromRoute] Guid id)
        {
            return View(new Ticket { ClientId = id });
        }

        [HttpPost]
        public async Task<IActionResult> AddTicket([FromForm] Ticket ticket)
        {
            ticket.DateAdded = DateTime.Now;
            ticket.IsUrgent = false;
            ticket.Open = true;

            _context.Tickets.Add(ticket);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(TicketsController.Open), "Tickets", new { id = ticket.Id });
        }
    }
}