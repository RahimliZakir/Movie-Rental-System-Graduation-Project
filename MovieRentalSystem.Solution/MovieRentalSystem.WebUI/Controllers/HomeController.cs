using Microsoft.AspNetCore.Mvc;

namespace MovieRentalSystem.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult ContactUs()
        {

            return View();
        }

        public IActionResult ComingSoon()
        {

            return View();
        }

        public IActionResult FAQ()
        {

            return View();
        }
    }
}
