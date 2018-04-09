using System;
using System.Threading.Tasks;
using GoldenTicket.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoldenTicket.WebApi.Controllers
{
    [Route("[controller]")]
    public class TechniciansController : Controller
    {
        private GoldenTicketContext _context;

        public TechniciansController(GoldenTicketContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Technicians.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            return ticket == null ? NotFound(new Ticket { Id = id }) : Ok(ticket) as IActionResult;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] Technician technician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Technicians.Add(technician);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = technician.Id }, technician);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync([FromRoute] Guid id)
        {
            var technician = await _context.Technicians.FindAsync(id);
            if (technician == null)
            {
                return NotFound(new Technician { Id = id });
            }
            _context.Technicians.Remove(technician);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}