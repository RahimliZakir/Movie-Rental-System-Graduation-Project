using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Modules.GenresModule;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.ViewModels;

namespace MovieRentalSystem.WebUI.Controllers
{
    public class ShowController : Controller
    {
        readonly IMediator mediator;
        readonly IConfiguration conf;

        public ShowController(IMediator mediator, IConfiguration conf)
        {
            this.mediator = mediator;
            this.conf = conf;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            ShowViewModel vm = new();

            int showParentId = conf.GetValue<int>("Genres:ShowParentId");

            IEnumerable<Genre> genres = await mediator.Send(new GenreGetAllActiveQuery());

            vm.Genres = genres.Where(g => g.ParentId.Equals(showParentId));

            return View(vm);
        }

        [AllowAnonymous]
        public IActionResult Details()
        {
            return View();
        }
    }
}
