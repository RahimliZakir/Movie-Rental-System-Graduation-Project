using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.Models.Entities.Membership;
using MovieRentalSystem.WebUI.Models.FormModels;

namespace MovieRentalSystem.WebUI.AppCode.Modules.AccountModule
{
    public class ResetPasswordCommand : ResetPasswordFormModel, IRequest<bool>
    {
        public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, bool>
        {
            readonly IActionContextAccessor ctx;
            readonly UserManager<AppUser> userManager;

            public ResetPasswordHandler(IActionContextAccessor ctx, UserManager<AppUser> userManager)
            {
                this.ctx = ctx;
                this.userManager = userManager;
            }

            async public Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
            {
                AppUser user = await userManager.FindByEmailAsync(request.Email);

                if (user != null)
                {
                    IdentityResult resetStatus = await userManager.ResetPasswordAsync(user, request.Token, request.Password);

                    if (resetStatus.Succeeded)
                    {
                        return true;
                    }
                    else
                    {
                        foreach (var item in resetStatus.Errors)
                        {
                            ctx.AddModelError("", item.Description);
                        }
                    }
                }

                return false;
            }
        }
    }
}
