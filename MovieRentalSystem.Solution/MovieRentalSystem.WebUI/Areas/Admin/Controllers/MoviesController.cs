using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.DirectorsModule;
using MovieRentalSystem.WebUI.AppCode.Modules.MoviesModule;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "movies.config")]
    public class MoviesController : Controller
    {
        readonly IMediator mediator;

        public MoviesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index(MoviePagedQuery query)
        {
            query.Response = await mediator.Send(query);

            return View(query);
        }

        public async Task<IActionResult> Details(MovieSingleQuery query)
        {
            MovieViewModel movie = await mediator.Send(query);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Directors = new SelectList(await mediator.Send(new DirectorGetAllActiveQuery()), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,IsPremium,Price,MovieFrame,Duration,Quality,DirectorId")] MovieCreateCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        public async Task<IActionResult> Edit(MovieSingleQuery query)
        {
            MovieViewModel movie = await mediator.Send(query);

            ViewBag.Directors = new SelectList(await mediator.Send(new DirectorGetAllActiveQuery()), "Id", "Name", movie.Id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,Description,IsPremium,Duration,Price,MovieFrame,Quality,DirectorId,Id")] MovieEditCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(MovieRemoveCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
