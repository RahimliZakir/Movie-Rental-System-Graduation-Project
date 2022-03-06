using Microsoft.AspNetCore.Mvc;

namespace MovieRentalSystem.WebUI.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
