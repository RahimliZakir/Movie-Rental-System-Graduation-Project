using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.AppCode.Modules.AccountModule
{
    public class SetEmailConfirmedCommand : SetEmailConfirmedFormModel, IRequest<CommandJsonResponse>
    {
        public class SetEmailConfirmedCommandHandler : IRequestHandler<SetEmailConfirmedCommand, CommandJsonResponse>
        {
            readonly IActionContextAccessor ctx;
            readonly UserManager<AppUser> userManager;

            public SetEmailConfirmedCommandHandler(IActionContextAccessor ctx, UserManager<AppUser> userManager)
            {
                this.ctx = ctx;
                this.userManager = userManager;
            }

            async public Task<CommandJsonResponse> Handle(SetEmailConfirmedCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                string email = request.Email;
                string token = request.Token;

                if (email == null || token == null)
                {
                    ctx.AddModelError("SetEmailConfirmedError", "Xəta baş verdi!");
                }
                else
                {
                    AppUser user = await userManager.FindByEmailAsync(email);
                    IdentityResult emailConfirmresult = await userManager.ConfirmEmailAsync(user, token);

                    if (emailConfirmresult.Succeeded)
                    {
                        response.Error = false;
                        response.Message = "Emailiniz uğurla təsdiqləndi!";
                    }
                    else
                    {
                        foreach (IdentityError error in emailConfirmresult.Errors)
                        {
                            ctx.AddModelError("SetEmailConfirmedError", error.Description);
                        }

                        response.Error = true;
                        response.Message = "Emailiniz təsdiqlənən zaman xəta baş verdi!";
                    }
                }

                return response;
            }
        }
    }
}
