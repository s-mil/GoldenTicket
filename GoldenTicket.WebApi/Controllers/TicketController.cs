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
        [HttpGet]
        public async Task<IActionResult> GetTickets([FromServices] GoldenTicketContext context)
        {
            var orderedTickets = await context.Tickets
                .OrderByDescending(ticket => ticket.DateAdded)
                .GroupBy(ticket => ticket.ClientId)
                .OrderBy(ticketClientGroup => ticketClientGroup.Count())
                .SelectMany(ticketClientGroup => ticketClientGroup)
                .OrderByDescending(ticket => ticket.IsUrgent)
                .ToListAsync();

            return Ok(orderedTickets);
        }
    }
}