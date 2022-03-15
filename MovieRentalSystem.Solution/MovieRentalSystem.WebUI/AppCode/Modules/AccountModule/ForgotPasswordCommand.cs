using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities.Membership;
using MovieRentalSystem.WebUI.Models.FormModels;

namespace MovieRentalSystem.WebUI.AppCode.Modules.AccountModule
{
    public class ForgotPasswordCommand : ForgotPasswordFormModel, IRequest<CommandJsonResponse>
    {
        public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, CommandJsonResponse>
        {
            readonly IActionContextAccessor ctx;
            readonly UserManager<AppUser> userManager;
            readonly IConfiguration conf;
            readonly IUrlHelperFactory factory;

            public ForgotPasswordCommandHandler(IActionContextAccessor ctx, UserManager<AppUser> userManager, IConfiguration conf, IUrlHelperFactory factory)
            {
                this.ctx = ctx;
                this.userManager = userManager;
                this.conf = conf;
                this.factory = factory;
            }

            async public Task<CommandJsonResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (ctx.IsValid())
                {
                    AppUser user = await userManager.FindByEmailAsync(request.Email);

                    string token = await userManager.GeneratePasswordResetTokenAsync(user);

                    ActionContext context = ctx.ActionContext;
                    IUrlHelper url = factory.GetUrlHelper(context);

                    string passwordResetLink = url.Action("ResetPassword", "Account", new
                    {
                        email = user.Email,
                        token = token
                    }, ctx.GetHttpContext().Request.Scheme);

                    string fromMail = conf.GetValue<string>("FactoryCredentials:Email");
                    string pwd = conf.GetValue<string>("FactoryCredentials:Password");
                    string? cc = conf.GetValue<string>("FactoryCredentials:CC");
                    string subject = "Şifrə yeniləmə linki.";
                    string body = $"<a href='{passwordResetLink}'>Bura</a> klik edərək şifrə yeniləmə pəncərəsinə yönələ bilərsiniz.";

                    bool status = conf.SendMail(fromMail, pwd, request.Email, subject, body, true, cc);

                    if (status)
                    {
                        response.Error = false;
                        response.Message = "Şifrə yeniləmə linki mailinizə göndərildi!";
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Xəta baş verdi bir neçə dəqiqədən sonra yenidən yoxlayın!";
                    }
                }

                return response;
            }
        }
    }
}
