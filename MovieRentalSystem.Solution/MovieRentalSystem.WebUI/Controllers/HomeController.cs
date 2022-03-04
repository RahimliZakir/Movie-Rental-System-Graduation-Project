using Microsoft.AspNetCore.Mvc;

namespace MovieRentalSystem.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
