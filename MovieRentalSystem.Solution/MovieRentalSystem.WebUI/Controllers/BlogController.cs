using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Modules.BlogModule;
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
    }
}
