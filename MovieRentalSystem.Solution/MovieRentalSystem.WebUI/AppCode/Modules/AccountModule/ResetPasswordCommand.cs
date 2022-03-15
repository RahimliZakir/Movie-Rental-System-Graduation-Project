using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.Entities.Membership;
using MovieRentalSystem.WebUI.Models.FormModels;

namespace MovieRentalSystem.WebUI.AppCode.Modules.AccountModule
{
    public class ResetPasswordCommand : ResetPasswordFormModel, IRequest<CommandJsonResponse>
    {
        public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, CommandJsonResponse>
        {
            readonly IActionContextAccessor ctx;
            readonly UserManager<AppUser> userManager;

            public ResetPasswordHandler(IActionContextAccessor ctx, UserManager<AppUser> userManager)
            {
                this.ctx = ctx;
                this.userManager = userManager;
            }

            async public Task<CommandJsonResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                AppUser user = await userManager.FindByEmailAsync(request.Email);

                if (user != null)
                {
                    IdentityResult resetStatus = await userManager.ResetPasswordAsync(user, request.Token, request.Password);

                    if (resetStatus.Succeeded)
                    {
                        response.Error = false;
                        response.Message = "Şifrə uğurla yeniləndi!";
                        goto end;
                    }
                    else
                    {
                        foreach (var item in resetStatus.Errors)
                        {
                            ctx.AddModelError("", item.Description);
                        }

                        response.Error = true;
                        response.Message = "Şifrə yenilənən zaman xəta baş verdi!";
                        goto end;
                    }
                }

            end:
                return response;
            }
        }
    }
}
