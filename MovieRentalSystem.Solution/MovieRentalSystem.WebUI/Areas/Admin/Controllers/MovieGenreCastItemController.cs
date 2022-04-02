using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.CastsModule;
using MovieRentalSystem.WebUI.AppCode.Modules.GenresModule;
using MovieRentalSystem.WebUI.AppCode.Modules.MovieGenreCastItemModule;
using MovieRentalSystem.WebUI.AppCode.Modules.MoviesModule;
using MovieRentalSystem.WebUI.AppCode.Modules.ShowGenreCastItemModule;
using MovieRentalSystem.WebUI.AppCode.Modules.ShowsModule;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "moviegenrecastitems.config")]
    public class MovieGenreCastItemController : Controller
    {
        readonly IMediator mediator;
        readonly IConfiguration conf;

        public MovieGenreCastItemController(IMediator mediator, IConfiguration conf)
        {
            this.mediator = mediator;
            this.conf = conf;
        }

        async public Task<IActionResult> Index()
        {
            var query = new MovieGenreCastItemGetAllActiveQuery();

            IEnumerable<Movie> movies = await mediator.Send(query);

            return View(movies);
        }

        async public Task<IActionResult> Details(MovieGenreCastItemSingleQuery query)
        {
            MovieGenreCastItemFormModel data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            MovieGetAllActiveQuery moviesQuery = new();
            IEnumerable<Movie> movies = await mediator.Send(moviesQuery);
            ViewBag.Movies = new SelectList(movies, "Id", "Name");

            int showParentId = conf.GetValue<int>("Genres:FilmParentId");
            GenreGetAllActiveQuery genresQuery = new();
            IEnumerable<Genre> genres = await mediator.Send(genresQuery);
            genres = genres.Where(g => g.ParentId == showParentId);
            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            CastGetAllActiveQuery castsQuery = new();
            IEnumerable<Cast> casts = await mediator.Send(castsQuery);
            ViewBag.Casts = new SelectList(casts, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieGenreCastItemCreateCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            if (!response.Error)
            {
                return Json(response);
            }

            return View(request);
        }

        async public Task<IActionResult> Edit(MovieGenreCastItemSingleQuery query)
        {
            MovieGenreCastItemFormModel data = await mediator.Send(query);

            MovieGetAllActiveQuery moviesQuery = new();
            IEnumerable<Movie> movies = await mediator.Send(moviesQuery);
            ViewBag.Movies = new SelectList(movies, "Id", "Name", data.Movie.Id);

            int showParentId = conf.GetValue<int>("Genres:FilmParentId");
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
        async public Task<IActionResult> Edit(MovieGenreCastItemEditCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            if (response.Error == false)
                return Json(response);

            return View(request);
        }

        [HttpPost]
        async public Task<IActionResult> Delete(MovieGenreCastItemRemoveCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
