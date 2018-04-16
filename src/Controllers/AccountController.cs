using System.Threading.Tasks;
using GoldenTicket.Models;
using GoldenTicket.Models.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GoldenTicket.Controllers
{
    /// <summary>
    /// Controller for accounts
    /// </summary>
    public class AccountController : Controller
    {
        private readonly SignInManager<Technician> _signInManager;

        private readonly ILogger _logger;

        /// <summary>
        /// Initializes the Account Controller
        /// </summary>
        /// <param name="signInManager">manages sign in and out</param>
        /// <param name="logger">logs</param>
        public AccountController(SignInManager<Technician> signInManager, ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Handles logout
        /// </summary>
        /// <returns>login page</returns>
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            await _signInManager.SignOutAsync();
            return View();
        }

        /// <summary>
        /// Logs the user out.
        /// </summary>
        /// <returns>Redirect to login</returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        /// <summary>
        /// Method for logging in
        /// </summary>
        /// <param name="loginRequest">login information</param>
        /// <param name="returnUrl">URL to return to after login</param>
        /// <returns>login request page</returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest, [FromQuery] string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequest);
            }

            var result = await _signInManager.PasswordSignInAsync(loginRequest.Username, loginRequest.Password, loginRequest.RememberMe, false);

            if (result.Succeeded)
            {
                _logger.LogInformation($"{User.Identity.Name} logged in.");
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(TicketsController.All), "Tickets");
                }
            }

            return View(loginRequest);
        }

        /// <summary>
        /// Denies access
        /// </summary>
        /// <returns>Access denied page</returns>
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}