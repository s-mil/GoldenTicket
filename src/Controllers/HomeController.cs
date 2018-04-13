using System.Diagnostics;
using GoldenTicket.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoldenTicket.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
