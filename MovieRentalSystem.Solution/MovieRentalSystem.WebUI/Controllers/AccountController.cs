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

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        async public Task<IActionResult> ForgotPassword(ForgotPasswordCommand request)
        {
            bool result = await mediator.Send(request);

            if (result)
                return RedirectToAction(nameof(ResetPassword));

            return View();
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
            bool result = await mediator.Send(request);

            if (result)
                return Redirect(@"\signin.html");

            return View();
        }
    }
}
