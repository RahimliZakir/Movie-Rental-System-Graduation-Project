using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.AutoMapper.Dtos;
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
        readonly IMapper mapper;

        public BlogController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await mediator.Send(new BlogGetAllActiveQuery());

            IEnumerable<BlogDto> blogDtos = mapper.Map<IEnumerable<BlogDto>>(blogs);

            return View(blogDtos);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(BlogSingleQuery query)
        {
            Blog blog = mapper.Map<Blog>(await mediator.Send(query));
            BlogDto blogDto = mapper.Map<BlogDto>(blog);

            return View(blogDto);
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
