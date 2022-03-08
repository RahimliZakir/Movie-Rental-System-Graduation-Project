using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Modules.FaqsModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly IMediator mediator;

        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

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

        public async Task<IActionResult> Faq()
        {
            IEnumerable<Faq> faqs = await mediator.Send(new FaqGetAllActiveQuery());

            return View(faqs);
        }
    }
}
