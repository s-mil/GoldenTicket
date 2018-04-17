using System;
using System.Linq;
using System.Threading.Tasks;
using GoldenTicket.Data;
using GoldenTicket.Models;
using GoldenTicket.Models.TicketsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        private UserManager<Technician> _userManager;

        /// <summary>
        /// Initializes private variable _context
        /// </summary>
        /// <param name="context">context of current ticket</param>
        /// <param name="userManager">The user manager</param>
        public TicketsController(GoldenTicketContext context, UserManager<Technician> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        /// <summary>
        /// Open 
        /// </summary>
        /// <param name="id">The id of the ticket.</param>
        /// <returns>The add time view</returns>
        [HttpGet]
        public async Task<IActionResult> AddTime([FromRoute] Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            return View(new TicketTime { TicketTitle = ticket.Title, TicketId = ticket.Id });
        }

        [HttpPost]
        public async Task<IActionResult> AddTime([FromForm] TicketTime time)
        {
            _context.TechnicianTicketTimes.Add(new TechnicianTicketTime
            {
                End = time.End,
                Start = time.Start,
                TicketId = time.TicketId,
                TechnicianId = _userManager.GetUserName(User)
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Open), new { id = time.TicketId });
        }
    }
}