using AdminService;
using AdminService.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;


namespace Admin_Portal.Controllers
{
    public class AdminLoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IAccountService _accountservice;

        public AdminLoginController(ILoginService loginService, IAccountService accountservice)
        {
            _loginService = loginService;
            _accountservice = accountservice;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("DashBoard", "DashBoard");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (_loginService.Login(username, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin"),
                 
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return RedirectToAction("DashBoard", "DashBoard");
            }
            else
            {
                Log.Warning("Unsuccessful login attempt for username: {Username}", username);
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                ViewBag.ShowAlert = true;
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "AdminLogin");
        }

    }

}
