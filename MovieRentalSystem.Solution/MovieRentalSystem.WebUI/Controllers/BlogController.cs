using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.BlogCommentModule;
using MovieRentalSystem.WebUI.AppCode.Modules.BlogLikeModule;
using MovieRentalSystem.WebUI.AppCode.Modules.BlogModule;
using MovieRentalSystem.WebUI.AppCode.Modules.BlogUnlikeModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Controllers
{
    public class BlogController : Controller
    {
        readonly IMediator mediator;

        public BlogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await mediator.Send(new BlogGetAllActiveQuery());

            return View(blogs);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(BlogSingleQuery query)
        {
            BlogViewModel blog = await mediator.Send(query);

            return View(blog);
        }

        [HttpPost]
        [Authorize]
        async public Task<IActionResult> BlogLike(BlogLikeCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        [HttpPost]
        [Authorize]
        async public Task<IActionResult> BlogUnlike(BlogUnlikeCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        [HttpPost]
        [Authorize]
        async public Task<IActionResult> BlogComment(BlogCommentCommand request)
        {
            BlogComment blogComment = await mediator.Send(request);

            if (blogComment.ParentId.HasValue && blogComment.ParentId > 0)
            {
                Response.Headers.Add("BlogCommentParentId", blogComment.ParentId.ToString());
            }

            return PartialView("_BlogComment", blogComment);
        }

        [Authorize]
        async public Task<IActionResult> BlogCommentCount(BlogCommentCountCommand request)
        {
            int count = await mediator.Send(request);

            return Json(count);
        }
    }
}
