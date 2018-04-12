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
            return Ok(await _context.Set<Technician>().ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var technician = await _context.Set<Technician>().FindAsync(id);
            return technician == null ? NotFound(new Technician { Id = id }) : Ok(technician) as IActionResult;
        }
    }
}