using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;

namespace MovieRentalSystem.WebUI.Controllers
{
    public class ShowController : Controller
    {
        readonly MovieDbContext db;

        public ShowController(MovieDbContext db)
        {
            this.db = db;
        }

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
