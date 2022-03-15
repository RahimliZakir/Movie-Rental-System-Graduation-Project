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
    public class RegisterCommand : RegisterFormModel, IRequest<CommandJsonResponse>
    {
        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly UserManager<AppUser> userManager;

            public RegisterCommandHandler(MovieDbContext db, IActionContextAccessor ctx, UserManager<AppUser> userManager)
            {
                this.db = db;
                this.ctx = ctx;
                this.userManager = userManager;
            }

            async public Task<CommandJsonResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                AppUser appUser = new()
                {
                    UserName = request.Username,
                    Email = request.Email
                };

                IdentityResult appResponse = await userManager.CreateAsync(appUser, request.Password);

                if (appResponse.Succeeded)
                {
                    string url = @"\signin.html";

                    response.Error = false;
                    response.Message = url;
                }
                else
                {
                    foreach (IdentityError error in appResponse.Errors)
                    {
                        ctx.AddModelError("RegisterError", error.Description);
                    }
                }

                return response;
            }
        }
    }
}
