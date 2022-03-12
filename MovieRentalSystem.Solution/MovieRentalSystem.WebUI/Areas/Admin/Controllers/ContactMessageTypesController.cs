#nullable disable
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.ContactMessageTypesModule;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactMessageTypesController : Controller
    {
        readonly IMediator mediator;

        public ContactMessageTypesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index(ContactMessageTypePagedQuery query)
        {
            query.Response = await mediator.Send(query);

            return View(query);
        }

        public async Task<IActionResult> Details(ContactMessageTypeSingleQuery query)
        {
            ContactMessageTypeViewModel type = await mediator.Send(query);

            if (type == null)
            {
                return NotFound();
            }

            return View(type);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Text")] ContactMessageTypeCreateCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        public async Task<IActionResult> Edit(ContactMessageTypeSingleQuery query)
        {
            ContactMessageTypeViewModel type = await mediator.Send(query);

            if (type == null)
            {
                return NotFound();
            }

            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Text,Id")] ContactMessageTypeEditCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ContactMessageTypeRemoveCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
