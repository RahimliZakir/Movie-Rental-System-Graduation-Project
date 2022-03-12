using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.FaqsModule;
using MovieRentalSystem.WebUI.AppCode.Modules.SubscriptionsModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly IMediator mediator;
        readonly IConfiguration config;

        public HomeController(IMediator mediator, IConfiguration config)
        {
            this.mediator = mediator;
            this.config = config;
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

        [HttpPost]
        async public Task<IActionResult> Subscribe(SubscriptionCreateCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        [HttpGet("subscribe-route")]
        async public Task<IActionResult> ConfirmSubscription(ConfirmSubscriptionCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            if (response.Error)
                ViewBag.ResponseError = response.Message;
            else
                ViewBag.ResponseSuccess = response.Message;

            return View();
        }
    }
}
