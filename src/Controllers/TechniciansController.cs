using System;
using System.Threading.Tasks;
using GoldenTicket.Data;
using GoldenTicket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoldenTicket.Controllers
{
    /// <summary>
    /// Controller for technicians
    /// </summary>
    [Route("[controller]")]
    public class TechniciansController : Controller
    {
        private GoldenTicketContext _context;

        /// <summary>
        /// intializes _context
        /// </summary>
        /// <param name="context">context of the technician</param>
        public TechniciansController(GoldenTicketContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds tech to index
        /// </summary>
        /// <returns>Http OK response</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Set<Technician>().ToListAsync());
        }

        /// <summary>
        /// Gets the tech from id
        /// </summary>
        /// <param name="id">Unique id for technician</param>
        /// <returns>404 error or Http response for success</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var technician = await _context.Set<Technician>().FindAsync(id);
            return technician == null ? NotFound(new Technician { Id = id }) : Ok(technician) as IActionResult;
        }
    }
}