using System.Diagnostics;
using GoldenTicket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoldenTicket.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<Technician> _signInManager;

        public HomeController(SignInManager<Technician> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }
            return View();
        }
    }
}
