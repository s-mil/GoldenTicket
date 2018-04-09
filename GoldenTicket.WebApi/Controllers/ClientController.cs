using System;
using System.Threading.Tasks;
using GoldenTicket.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoldenTicket.WebApi.Controllers
{
    public class ClientController : Controller
    {
        private GoldenTicketContext _context;

        public ClientController(GoldenTicketContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            return ticket == null ? NotFound(new Ticket { Id = id }) : Ok(ticket) as IActionResult;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] Client Client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Clients.Add(Client);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = Client.Id }, Client);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync([FromRoute] Guid id)
        {
            var Client = await _context.Clients.FindAsync(id);
            if (Client == null)
            {
                return NotFound(new Client { Id = id });
            }
            _context.Clients.Remove(Client);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}