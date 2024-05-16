using Microsoft.AspNetCore.Mvc;

namespace Bank_Portal.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Exception500Error()
        {
            return View();
        }
    }
}
