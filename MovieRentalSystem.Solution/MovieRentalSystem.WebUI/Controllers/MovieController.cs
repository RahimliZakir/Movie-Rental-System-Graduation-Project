using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Modules.GenresModule;
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

            return View(vm);
        }

        [AllowAnonymous]
        public IActionResult Details()
        {
            return View();
        }
    }
}
