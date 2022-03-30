using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            return View(vm);
        }
    }
}
