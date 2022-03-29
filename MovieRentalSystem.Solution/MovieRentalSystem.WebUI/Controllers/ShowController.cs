using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Modules.GenresModule;
using MovieRentalSystem.WebUI.AppCode.Modules.ShowCommentModule;
using MovieRentalSystem.WebUI.AppCode.Modules.ShowsModule;
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
            ShowGenreViewModel vm = new();

            int showParentId = conf.GetValue<int>("Genres:ShowParentId");

            IEnumerable<Genre> genres = await mediator.Send(new GenreGetAllActiveQuery());
            IEnumerable<Show> shows = await mediator.Send(new ShowGetAllActiveQuery());

            vm.Genres = genres.Where(g => g.ParentId.Equals(showParentId));
            vm.Shows = shows;

            return View(vm);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(ShowSingleQuery query)
        {
            ShowViewModel show = await mediator.Send(query);

            return View(show);
        }

        [HttpPost]
        [Authorize]
        async public Task<IActionResult> BlogComment(ShowCommentCommand request)
        {
            ShowComment showComment = await mediator.Send(request);

            if (showComment.ParentId.HasValue && showComment.ParentId > 0)
            {
                Response.Headers.Add("ShowCommentParentId", showComment.ParentId.ToString());
            }

            return PartialView("_ShowComment", showComment);
        }
    }
}
