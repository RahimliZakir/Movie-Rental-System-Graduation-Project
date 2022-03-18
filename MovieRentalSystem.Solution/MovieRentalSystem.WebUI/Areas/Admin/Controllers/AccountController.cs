using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.AccountModule;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        readonly SignInManager<AppUser> signInManager;
        readonly IMediator mediator;

        public AccountController(SignInManager<AppUser> signInManager, IMediator mediator)
        {
            this.signInManager = signInManager;
            this.mediator = mediator;
        }

        async public Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(Index), "Home", new
            {
                area = ""
            });
        }

        public IActionResult ConfirmEmail()
        {
            return View();
        }

        [HttpPost]
        async public Task<IActionResult> ConfirmEmail(ConfirmEmailCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);


            if (!string.IsNullOrWhiteSpace(response.Temp))
            {
                TempData["ConfirmEmailError"] = response.Temp;

                return View();
            }
            else
            {
                return Json(response);
            }
        }

        async public Task<IActionResult> SetEmailConfirmed(SetEmailConfirmedCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return View(response);
        }
    }
}
