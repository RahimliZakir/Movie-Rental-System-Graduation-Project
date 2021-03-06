#nullable disable
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.GenresModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "genres.config")]
    public class GenresController : Controller
    {
        readonly IMediator mediator;

        public GenresController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Genre> genres = await mediator.Send(new GenreGetAllActiveQuery());

            return View(genres);
        }

        public async Task<IActionResult> Details(GenreSingleQuery query)
        {
            GenreViewModel genre = await mediator.Send(query);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<Genre> genres = await mediator.Send(new GenreGetAllActiveQuery());
            genres = genres.Where(g => g.ParentId == null).ToList();

            ViewBag.Parents = new SelectList(genres, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ParentId")] GenreCreateCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        public async Task<IActionResult> Edit(GenreSingleQuery query)
        {
            GenreViewModel genre = await mediator.Send(query);

            IEnumerable<Genre> genres = await mediator.Send(new GenreGetAllActiveQuery());
            genres = genres.Where(g => g.ParentId == null).ToList();
            ViewBag.Parents = new SelectList(genres, "Id", "Name", genre.ParentId);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,ParentId,Id")] GenreEditCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(GenreRemoveCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
