using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return View();
        }
    }
}
