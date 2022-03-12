#nullable disable
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.SubscriptionsModule;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscriptionsController : Controller
    {
        readonly IMediator mediator;

        public SubscriptionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index(SubscriptionPagedQuery query)
        {
            query.Response = await mediator.Send(query);

            return View(query);
        }

        public async Task<IActionResult> Details(SubscriptionSingleQuery query)
        {
            SubscriptionViewModel subscription = await mediator.Send(query);

            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SubscriptionRemoveCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
