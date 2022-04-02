using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Modules.GenresModule;
using MovieRentalSystem.WebUI.AppCode.Modules.MovieCommentModule;
using MovieRentalSystem.WebUI.AppCode.Modules.MoviesModule;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.ViewModels;

namespace MovieRentalSystem.WebUI.Controllers
{
    public class MovieController : Controller
    {
        readonly IMediator mediator;
        readonly IConfiguration conf;

        public MovieController(IMediator mediator, IConfiguration conf)
        {
            this.mediator = mediator;
            this.conf = conf;
        }

        [AllowAnonymous]
        async public Task<IActionResult> Index()
        {
            FilmViewModel vm = new();

            int filmParentId = conf.GetValue<int>("Genres:FilmParentId");

            IEnumerable<Genre> genres = await mediator.Send(new GenreGetAllActiveQuery());
            vm.Genres = genres.Where(g => g.ParentId.Equals(filmParentId));

            IEnumerable<Movie> movies = await mediator.Send(new MovieGetAllActiveQuery());
            vm.Movies = movies;

            return View(vm);
        }

        [AllowAnonymous]
        async public Task<IActionResult> Details(MovieSingleQuery query)
        {
            FilmViewModel vm = new();

            MovieViewModel movie = await mediator.Send(query);
            IEnumerable<Movie> relatedMovies = await mediator.Send(new MovieGetAllActiveQuery());
            relatedMovies = relatedMovies.Where(r => r.Id != movie.Id).Take(12).ToList();

            vm.Movie = movie;
            vm.RelatedMovies = relatedMovies;

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        async public Task<IActionResult> MovieComment(MovieCommentCommand request)
        {
            MovieComment movieComment = await mediator.Send(request);

            if (movieComment.ParentId.HasValue && movieComment.ParentId > 0)
            {
                Response.Headers.Add("MovieCommentParentId", movieComment.ParentId.ToString());
            }

            return PartialView("_MovieComment", movieComment);
        }

        [Authorize]
        async public Task<IActionResult> MovieCommentCount(MovieCommentCountCommand request)
        {
            int count = await mediator.Send(request);

            return Json(count);
        }
    }
}
