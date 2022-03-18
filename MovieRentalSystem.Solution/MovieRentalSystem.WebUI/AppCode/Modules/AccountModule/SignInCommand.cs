using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities.Membership;
using MovieRentalSystem.WebUI.Models.FormModels;

namespace MovieRentalSystem.WebUI.AppCode.Modules.AccountModule
{
    public class SignInCommand : SignInFormModel, IRequest<CommandJsonResponse>
    {
        public class SignInCommandHandler : IRequestHandler<SignInCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly UserManager<AppUser> userManager;
            readonly SignInManager<AppUser> signInManager;
            readonly IActionContextAccessor ctx;

            public SignInCommandHandler(MovieDbContext db, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IActionContextAccessor ctx)
            {
                this.db = db;
                this.userManager = userManager;
                this.signInManager = signInManager;
                this.ctx = ctx;
            }

            async public Task<CommandJsonResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                AppUser userResult = null;

                if (request.Username.IsEmail())
                {
                    userResult = await userManager.FindByEmailAsync(request.Username);
                }
                else
                {
                    userResult = await userManager.FindByNameAsync(request.Username);
                }

                if (userResult != null)
                {
                    SignInResult signInResult = await signInManager.PasswordSignInAsync(userResult, request.Password, request.RememberMe, false);

                    if (signInResult.Succeeded)
                    {
                        string returlUrl = ctx.GetHttpContext().Request.Query["returnUrl"];

                        if (!string.IsNullOrWhiteSpace(returlUrl))
                        {
                            response.Error = false;
                            response.ReturnUrl = returlUrl;
                            goto stopGenerate;
                        }
                        else
                        {
                            response.Error = false;
                            response.Message = "Xoş gəlmişsiniz!";
                            goto stopGenerate;
                        }
                    }
                    else
                    {
                        ctx.AddModelError("SignError", "İstifadəçi adı və ya şifrə səhvdir.");
                        response.Error = true;
                        response.Temp = "İstifadəçi adı və ya şifrə səhvdir.";
                    }
                }
                else
                {
                    ctx.AddModelError("SignError", "İstifadəçi adı və ya şifrə səhvdir.");
                    response.Error = true;
                    response.Temp = "İstifadəçi adı və ya şifrə səhvdir.";
                }

            stopGenerate:
                return response;
            }
        }
    }
}
