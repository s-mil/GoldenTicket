using System;
using System.Linq;
using System.Threading.Tasks;
using GoldenTicket.Data;
using GoldenTicket.Models;
using GoldenTicket.Models.TicketsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoldenTicket.Controllers
{
    /// <summary>
    /// Controller for managing tickets
    /// </summary>
    [Authorize]
    public class TicketsController : Controller
    {
        private GoldenTicketContext _context;
        /// <summary>
        /// Initializes private variable _context
        /// </summary>
        /// <param name="context">context of current ticket</param>
        public TicketsController(GoldenTicketContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Used to view all tickets in queue
        /// </summary>
        /// <param name="includeClosed">boolean for including closed tickets</param>
        /// <returns>view list of ordered tickets</returns>
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] bool includeClosed = false)
        {
            var orderedTickets = await _context.Tickets
                .OrderByDescending(ticket => ticket.DateAdded)
                .GroupBy(ticket => ticket.ClientId)
                .OrderBy(ticketClientGroup => ticketClientGroup.Count())
                .SelectMany(ticketClientGroup => ticketClientGroup)
                .Where(ticket => ticket.Open || ticket.Open != includeClosed)
                .OrderByDescending(ticket => ticket.IsUrgent)
                .OrderByDescending(ticket => ticket.Open)
                .ToListAsync();

            ViewData["includeClosed"] = includeClosed;

            return View(orderedTickets);
        }

        /// <summary>
        /// Opens a ticket
        /// </summary>
        /// <param name="id">unique id of ticket</param>
        /// <returns>view of the ticket</returns>
        [HttpGet]
        public async Task<IActionResult> Open([FromRoute] Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            var client = await _context.Clients.FindAsync(ticket.ClientId);
            var times = await _context.TechnicianTicketTimes.Where(time => time.TicketId == ticket.Id).Join(_context.Users, time => time.TechnicianId, tech => tech.UserName, (time, tech) => new TechnicianTime { Technician = tech, Time = time }).ToListAsync();
            return View(new TicketDetails { Ticket = ticket, Client = client, Times = times });
        }

        /// <summary>
        /// Open a ticket for editiing
        /// </summary>
        /// <param name="id">unique id of ticket</param>
        /// <returns>view of the ticket to edit</returns>
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            return View(ticket);
        }
    }
}