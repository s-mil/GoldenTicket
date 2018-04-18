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
        /// Opens a client's details
        /// </summary>
        /// <param name="id">The id of the client</param>
        /// <returns>The client</returns>
        [HttpGet]
        public async Task<IActionResult> Open([FromRoute] Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            return View(client);
        }

        /// <summary>
        /// Gets the add client view
        /// </summary>
        /// <returns>The add client view.</returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Adds a client to the database
        /// </summary>
        /// <param name="client">The client to add</param>
        /// <returns>The added client</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Client client)
        {
            client.DateAdded = DateTime.Now;
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Open), new { id = client.Id });
        }

        /// <summary>
        /// Gets view for adding a ticket.
        /// </summary>
        /// <returns>The view.</returns>
        [HttpGet]
        public IActionResult AddTicket([FromRoute] Guid id)
        {
            return View(new Ticket { ClientId = id });
        }

        /// <summary>
        /// Adds a ticket
        /// </summary>
        /// <param name="ticket">The ticket to be added</param>
        /// <returns>The added ticket</returns>
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