using System.Threading.Tasks;
using GoldenTicket.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoldenTicket.WebApi.Controllers
{
    [Route("ticket")]
    public class TicketController : Controller
    {
        public async Task<IActionResult> GetTickets([FromServices] GoldenTicketContext context)
        {
            return Ok(await context.Tickets.ToListAsync());
        }
    }
}