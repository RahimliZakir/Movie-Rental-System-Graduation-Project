using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.AppCode.Modules.AccountModule
{
    public class ConfirmEmailCommand : ConfirmEmailFormModel, IRequest<CommandJsonResponse>
    {
        public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, CommandJsonResponse>
        {
            readonly IActionContextAccessor ctx;
            readonly UserManager<AppUser> userManager;
            readonly IUrlHelperFactory factory;
            readonly IConfiguration conf;

            public ConfirmEmailCommandHandler(IActionContextAccessor ctx, UserManager<AppUser> userManager, IUrlHelperFactory factory, IConfiguration conf)
            {
                this.ctx = ctx;
                this.userManager = userManager;
                this.factory = factory;
                this.conf = conf;
            }

            async public Task<CommandJsonResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                string email = request.Email;

                AppUser user = await userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    string token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    IUrlHelper url = factory.GetUrlHelper(ctx.ActionContext);

                    string confirmEmailLink = url.Action("SetEmailConfirmed", "Account", new
                    {
                        email = email,
                        token = token
                    }, ctx.GetHttpContext().Request.Scheme);

                    bool result = conf.SendMail(conf["FactoryCredentials:Email"], conf["FactoryCredentials:Password"], email, "Email təsdiqləmə linki.", $"<a href='{confirmEmailLink}'>Bura</a> klik edərək email təsdiqləmə pəncərəsinə yönələ bilərsiniz.", true);

                    if (result)
                    {
                        response.Error = false;
                        response.Message = "Email təsdiqləmə linki sizin email ünvanınıza göndərildi, təsdiq gözlənilir!";
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Email göndərilən zaman xəta baş verdi, bir-neçə dəqiqədən sonra yenidən yoxlayın!";
                    }
                }
                else
                {
                    response.Error = true;
                    response.Temp = "Belə bir istifadəçi yoxdur!";
                }

                return response;
            }
        }
    }
}
