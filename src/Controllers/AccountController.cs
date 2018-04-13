using System.Threading.Tasks;
using GoldenTicket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GoldenTicket.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Technician> _userManager;
        private readonly SignInManager<Technician> _signInManager;
        private readonly ILogger _logger;

        public AccountController(UserManager<Technician> userManager, SignInManager<Technician> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> LoginAsync()
        {
            await _signInManager.SignOutAsync();
            return View();
        }

        [HttpPost]
        public IActionResult Login()
        {
            return View();
        }
    }
}