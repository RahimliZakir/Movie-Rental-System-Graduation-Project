using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Modules.MoviesModule;
using MovieRentalSystem.WebUI.AppCode.Modules.ShowsModule;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.ViewModels;
using System.Linq;

namespace MovieRentalSystem.WebUI.Controllers
{
    [AllowAnonymous]
    public class WishlistController : Controller
    {
        readonly IMediator mediator;

        public WishlistController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async public Task<IActionResult> Index()
        {
            WishlistViewModel? vm = new();

            HttpContext.Request.Cookies.TryGetValue("wishlist-show", out string? wishlistShow);

            if (!string.IsNullOrWhiteSpace(wishlistShow))
            {
                int[]? selectedShowIds = wishlistShow?.Split(new[] { ',' })
                                                      .Select(b => int.Parse(b))
                                                      .ToArray();

                if (selectedShowIds?.Length > 0 && selectedShowIds != null)
                {
                    IEnumerable<Show>? shows = await mediator.Send(new ShowGetAllActiveQuery());
                    shows = shows.Where(s => selectedShowIds.Contains(s.Id));
                    vm.Shows = shows;
                }
            }

            HttpContext.Request.Cookies.TryGetValue("wishlist-movie", out string? wishlistMovie);

            if (!string.IsNullOrWhiteSpace(wishlistMovie))
            {
                int[]? selectedMovieIds = wishlistMovie?.Split(new[] { ',' })
                                                      .Select(b => int.Parse(b))
                                                      .ToArray();

                if (selectedMovieIds?.Length > 0 && selectedMovieIds != null)
                {
                    IEnumerable<Movie>? movies = await mediator.Send(new MovieGetAllActiveQuery());
                    movies = movies.Where(s => selectedMovieIds.Contains(s.Id));
                    vm.Movies = movies;
                }
            }

            return View(vm);
        }
    }
}
