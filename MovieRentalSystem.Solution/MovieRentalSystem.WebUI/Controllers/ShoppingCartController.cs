using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Modules.MoviesModule;
using MovieRentalSystem.WebUI.AppCode.Modules.ShowsModule;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.ViewModels;

namespace MovieRentalSystem.WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        readonly IMediator mediator;

        public ShoppingCartController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        async public Task<IActionResult> Index()
        {
            CheckoutViewModel? vm = new();

            HttpContext.Request.Cookies.TryGetValue("checkout-show", out string? checkoutShow);

            if (!string.IsNullOrWhiteSpace(checkoutShow))
            {
                int[]? selectedShowIds = checkoutShow?.Split(new[] { ',' })
                                                      .Select(b => int.Parse(b))
                                                      .ToArray();

                if (selectedShowIds?.Length > 0 && selectedShowIds != null)
                {
                    IEnumerable<Show>? shows = await mediator.Send(new ShowGetAllActiveQuery());
                    shows = shows.Where(s => selectedShowIds.Contains(s.Id));
                    vm.Shows = shows;
                }
            }

            HttpContext.Request.Cookies.TryGetValue("checkout-movie", out string? checkoutMovie);

            if (!string.IsNullOrWhiteSpace(checkoutMovie))
            {
                int[]? selectedMovieIds = checkoutMovie?.Split(new[] { ',' })
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

        [HttpPost]
        [Authorize]
        async public Task<IActionResult> MovieCheckout()
        {


            return Ok();
        }
    }
}
