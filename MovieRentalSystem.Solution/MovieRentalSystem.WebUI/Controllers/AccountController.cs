using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.AccountModule;

namespace MovieRentalSystem.WebUI.Controllers
{
    public class AccountController : Controller
    {
        readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        async public Task<IActionResult> SignIn(SignInCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            if (response.Error == false)
            {
                if (response.Message.ValidateUrl())
                {
                    return Redirect(response.Message);
                }
                else
                {
                    return RedirectToAction(nameof(Index), "Faqs", routeValues: new
                    {
                        area = "Admin"
                    });
                }
            }
            else
            {
                TempData["Status"] = response.Message;
            }

            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            if (!response.Error)
                return Redirect(response.Message);

            return View();
        }
    }
}
