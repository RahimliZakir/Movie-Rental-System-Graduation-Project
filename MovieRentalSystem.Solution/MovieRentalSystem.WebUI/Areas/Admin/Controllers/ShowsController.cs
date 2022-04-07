using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.DirectorsModule;
using MovieRentalSystem.WebUI.AppCode.Modules.RoomsModule;
using MovieRentalSystem.WebUI.AppCode.Modules.ShowsModule;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "shows.config")]
    public class ShowsController : Controller
    {
        readonly IMediator mediator;

        public ShowsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index(ShowPagedQuery query)
        {
            query.Response = await mediator.Send(query);

            return View(query);
        }

        public async Task<IActionResult> Details(ShowSingleQuery query)
        {
            ShowViewModel show = await mediator.Send(query);

            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Directors = new SelectList(await mediator.Send(new DirectorGetAllActiveQuery()), "Id", "Name");
            ViewBag.Rooms = new SelectList(await mediator.Send(new RoomGetAllActiveQuery()), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,IsPremium,RoomId,Price,File,Duration,Quality,DirectorId")] ShowCreateCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        public async Task<IActionResult> Edit(ShowSingleQuery query)
        {
            ShowViewModel show = await mediator.Send(query);

            ViewBag.Directors = new SelectList(await mediator.Send(new DirectorGetAllActiveQuery()), "Id", "Name", show.DirectorId);
            ViewBag.Rooms = new SelectList(await mediator.Send(new RoomGetAllActiveQuery()), "Id", "Name", show.RoomId);

            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,Description,IsPremium,RoomId,Duration,Price,File,FileTemp,Quality,DirectorId,Id")] ShowEditCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ShowRemoveCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
