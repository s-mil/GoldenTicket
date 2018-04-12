using System;
using System.Linq;
using System.Threading.Tasks;
using GoldenTicket.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoldenTicket.WebApi.Controllers
{
    [Route("[controller]")]
    public class TicketsController : Controller
    {
        private GoldenTicketContext _context;

        public TicketsController(GoldenTicketContext context)
        {
            _context = context;
        }

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket([FromRoute] Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            return ticket == null ? NotFound(new Ticket { Id = id }) : Ok(ticket) as IActionResult;
        }

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