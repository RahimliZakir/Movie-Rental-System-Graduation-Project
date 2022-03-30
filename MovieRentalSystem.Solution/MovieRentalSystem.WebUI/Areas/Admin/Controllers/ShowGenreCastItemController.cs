using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.CastsModule;
using MovieRentalSystem.WebUI.AppCode.Modules.GenresModule;
using MovieRentalSystem.WebUI.AppCode.Modules.ShowGenreCastItemModule;
using MovieRentalSystem.WebUI.AppCode.Modules.ShowsModule;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "showgenrecastitems.config")]
    public class ShowGenreCastItemController : Controller
    {
        readonly IMediator mediator;
        readonly IConfiguration conf;

        public ShowGenreCastItemController(IMediator mediator, IConfiguration conf)
        {
            this.mediator = mediator;
            this.conf = conf;
        }

        async public Task<IActionResult> Index()
        {
            var query = new ShowGenreCastItemGetAllActiveQuery();

            IEnumerable<Show> shows = await mediator.Send(query);

            return View(shows);
        }

        async public Task<IActionResult> Details(ShowGenreCastItemSingleQuery query)
        {
            ShowGenreCastItemFormModel data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            ShowGetAllActiveQuery showsQuery = new();
            IEnumerable<Show> shows = await mediator.Send(showsQuery);
            ViewBag.Shows = new SelectList(shows, "Id", "Name");

            int showParentId = conf.GetValue<int>("Genres:ShowParentId");
            GenreGetAllActiveQuery gernesQuery = new();
            IEnumerable<Genre> genres = await mediator.Send(gernesQuery);
            genres = genres.Where(g => g.ParentId == showParentId);
            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            CastGetAllActiveQuery castsQuery = new();
            IEnumerable<Cast> casts = await mediator.Send(castsQuery);
            ViewBag.Casts = new SelectList(casts, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShowGenreCastItemCreateCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            if (!response.Error)
            {
                return Json(response);
            }

            return View(request);
        }

        async public Task<IActionResult> Edit(ShowGenreCastItemSingleQuery query)
        {
            ShowGenreCastItemFormModel data = await mediator.Send(query);

            ShowGetAllActiveQuery showsQuery = new();
            IEnumerable<Show> shows = await mediator.Send(showsQuery);
            ViewBag.Shows = new SelectList(shows, "Id", "Name", data.Show.Id);

            int showParentId = conf.GetValue<int>("Genres:ShowParentId");
            GenreGetAllActiveQuery genresQuery = new();
            IEnumerable<Genre> genres = await mediator.Send(genresQuery);
            genres = genres.Where(g => g.ParentId == showParentId);
            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            CastGetAllActiveQuery castsQuery = new();
            IEnumerable<Cast> casts = await mediator.Send(castsQuery);
            ViewBag.Casts = new SelectList(casts, "Id", "Name");

            return View(data);
        }

        [HttpPost]
        async public Task<IActionResult> Edit(ShowGenreCastItemEditCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            if (response.Error == false)
                return Json(response);

            return View(request);
        }

        [HttpPost]
        async public Task<IActionResult> Delete(ShowGenreCastItemRemoveCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
