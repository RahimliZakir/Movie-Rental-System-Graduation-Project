using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Modules.CheckoutMovieModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PurchasedMoviesController : Controller
    {
        readonly IMediator mediator;

        public PurchasedMoviesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async public Task<IActionResult> Index()
        {
            List<CheckoutMovieViewModel> checkouts = await mediator.Send(new CheckoutMovieGetAllActiveQuery());

            return View(checkouts);
        }
    }
}
