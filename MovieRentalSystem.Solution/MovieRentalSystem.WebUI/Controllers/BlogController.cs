using Microsoft.AspNetCore.Mvc;

namespace MovieRentalSystem.WebUI.Controllers
{
    public class BlogController : Controller
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
