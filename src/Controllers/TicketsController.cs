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
        /// Opens a new ticket
        /// </summary>
        /// <param name="id">unique id of ticket</param>
        /// <returns>view of the new ticket</returns>
        [HttpGet]
        public async Task<IActionResult> Open([FromRoute] Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            var client = await _context.Clients.FindAsync(ticket.ClientId);
            var times = await _context.TechnicianTicketTimes.Where(time => time.TicketId == ticket.Id).Join(_context.Users, time => time.TechnicianId, tech => tech.UserName, (time, tech) => new TechnicianTime { Technician = tech, Time = time }).ToListAsync();
            return View(new TicketDetails { Ticket = ticket, Client = client, Times = times});
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
        /// Orders the tickets into a list
        /// </summary>
        /// <returns>ordered ticket list</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orderedTickets = await _context.Tickets
                .OrderByDescending(ticket => ticket.DateAdded)
                .GroupBy(ticket => ticket.ClientId)
                .OrderBy(ticketClientGroup => ticketClientGroup.Count())
                .SelectMany(ticketClientGroup => ticketClientGroup)
                .OrderByDescending(ticket => ticket.IsUrgent)
                .OrderByDescending(ticket => ticket.Open)
                .ToListAsync();

            return Ok(orderedTickets);
        }

        /// <summary>
        /// Gets a ticket
        /// </summary>
        /// <param name="id">unique id of ticket</param>
        /// <returns>error if ticket doesn't exist or success response</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket([FromRoute] Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            return ticket == null ? NotFound(new Ticket { Id = id }) : Ok(ticket) as IActionResult;
        }

        /// <summary>
        /// Adds a ticket to ticket list
        /// </summary>
        /// <param name="ticket">the ticket to be added</param>
        /// <returns>ticket added response</returns>
        [HttpPost]
        public async Task<IActionResult> AddTicketAsync([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
        }

        /// <summary>
        /// Updates a ticket
        /// </summary>
        /// <param name="ticketId">the ticket id</param>
        /// <param name="ticketUpdate">the update to the ticket</param>
        /// <returns>error if ticket doesn't exist or success response</returns>
        [HttpPut("{ticketId}")]
        public async Task<IActionResult> UpdateTicketAsync([FromRoute] Guid ticketId, [FromBody] Ticket ticketUpdate)
        {
            var ticketExists = await _context.Tickets.AnyAsync(ticket => ticket.Id == ticketId);
            if (!ticketExists)
            {
                return NotFound(new Ticket { Id = ticketId });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ticketUpdate.Id = ticketId;
            _context.Tickets.Update(ticketUpdate);
            await _context.SaveChangesAsync();
            return Ok(ticketUpdate);
        }

        /// <summary>
        /// Removes a ticket
        /// </summary>
        /// <param name="ticketId">unique id of ticket</param>
        /// <returns>if ticket exists, a no content reponse, else 404 error</returns>
        [HttpDelete("{ticketId}")]
        public async Task<IActionResult> RemoveTicketAsync([FromRoute] Guid ticketId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null)
            {
                return NotFound(new Ticket { Id = ticketId });
            }
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Adds time to a ticket
        /// </summary>
        /// <param name="ticketId">unique id of ticket</param>
        /// <param name="time">time for completion</param>
        /// <returns>time added successfully response if ticket exists, ticket state is valid, and tech exists</returns>
        [HttpPost("{ticketId}/time")]
        public async Task<IActionResult> AddTimeToTicketAsync([FromRoute] Guid ticketId, [FromBody] TechnicianTicketTime time)
        {
            var ticketExists = await _context.Tickets.AnyAsync(ticket => ticket.Id == ticketId);
            if (!ticketExists)
            {
                return NotFound(new Ticket { Id = ticketId });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var technicianExists = await _context.Set<Technician>().AnyAsync(technician => technician.Id == time.TechnicianId);
            if (!technicianExists)
            {
                return NotFound(new Technician { Id = time.TechnicianId });
            }
            _context.TechnicianTicketTimes.Add(time);
            await _context.SaveChangesAsync();
            return Ok(time);
        }

        /// <summary>
        /// Gets the time for a ticket
        /// </summary>
        /// <param name="ticketId">unique id of ticket</param>
        /// <returns>ticket time</returns>
        [HttpGet("{ticketId}/time")]
        public async Task<IActionResult> GetTimeForTicketAsync([FromRoute] Guid ticketId)
        {
            var ticketExists = await _context.Tickets.AnyAsync(ticket => ticket.Id == ticketId);
            if (!ticketExists)
            {
                return NotFound(new Ticket { Id = ticketId });
            }
            var times = _context.TechnicianTicketTimes.Where(t => t.TicketId == ticketId);
            return Ok(times);
        }
    }
}