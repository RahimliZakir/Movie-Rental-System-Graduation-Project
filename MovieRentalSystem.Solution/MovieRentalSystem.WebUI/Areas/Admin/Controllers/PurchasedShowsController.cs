using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Modules.CheckoutShowModule;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PurchasedShowsController : Controller
    {
        readonly IMediator mediator;

        public PurchasedShowsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async public Task<IActionResult> Index()
        {
            List<CheckoutShowViewModel> checkouts = await mediator.Send(new CheckoutShowGetAllActiveQuery());

            return View(checkouts);
        }
    }
}
