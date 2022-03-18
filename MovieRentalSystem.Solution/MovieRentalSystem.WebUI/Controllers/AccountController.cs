using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Modules.AccountModule;
using MovieRentalSystem.WebUI.Models.FormModels;

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
                    return RedirectToAction(nameof(Index), "PersonalSide", routeValues: new
                    {
                        area = "Admin"
                    });
                }
            }
            else
            {
                TempData["Status"] = response.Temp;
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

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        async public Task<IActionResult> ForgotPassword(ForgotPasswordCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            ResetPasswordFormModel formModel = new()
            {
                Token = token,
                Email = email
            };

            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Xəta!");
            }

            return View(formModel);
        }

        [HttpPost]
        [AllowAnonymous]
        async public Task<IActionResult> ResetPassword(ResetPasswordCommand request)
        {
            CommandJsonResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
