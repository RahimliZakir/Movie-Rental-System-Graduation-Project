using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.ContactMessagesModule;
using MovieRentalSystem.WebUI.AppCode.Modules.ContactMessageTypesModule;
using MovieRentalSystem.WebUI.AppCode.Modules.FaqsModule;
using MovieRentalSystem.WebUI.AppCode.Modules.MoviesModule;
using MovieRentalSystem.WebUI.AppCode.Modules.ShowsModule;
using MovieRentalSystem.WebUI.AppCode.Modules.SubscriptionsModule;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.ViewModels;

namespace MovieRentalSystem.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        readonly IMediator mediator;
        readonly IConfiguration config;

        public HomeController(IMediator mediator, IConfiguration config)
        {
            this.mediator = mediator;
            this.config = config;
        }

        async public Task<IActionResult> Index()
        {
            HomeViewModel vm = new();

            IEnumerable<Show> shows = await mediator.Send(new ShowGetAllActiveQuery());
            vm.Shows = shows.OrderByDescending(m => m.Id).Take(3);

            IEnumerable<Movie> latestMovies = await mediator.Send(new MovieGetAllActiveQuery());
            vm.LatestMovies = latestMovies.OrderByDescending(m => m.Id);
            vm.LatestShows = shows.OrderByDescending(s => s.Id);

            return View(vm);
        }

        public async Task<IActionResult> ContactUs()
        {
            ViewBag.Types = new SelectList(await mediator.Send(new ContactMessageTypeGetAllActiveQuery()), "Id", "Text");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(ContactMessageCreateCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
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
