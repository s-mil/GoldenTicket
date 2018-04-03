using System;
using System.Linq;
using System.Threading.Tasks;
using GoldenTicket.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoldenTicket.WebApi.Controllers
{
    [Route("tickets")]
    public class TicketController : Controller
    {
        private GoldenTicketContext _context;

        public TicketController(GoldenTicketContext context)
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
            return CreatedAtAction(nameof(GetTicket), new {id = ticket.Id}, ticket);
        }
    }
}