using Microsoft.AspNetCore.Mvc;

namespace PODBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}