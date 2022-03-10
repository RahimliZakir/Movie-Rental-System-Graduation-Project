using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.SubscripitionsModule
{
    public class SubscriptionCreateCommand : SubscriptionViewModel, IRequest<CommandJsonResponse>
    {
        public class SubscriptionCreateCommandHandler : IRequestHandler<SubscriptionCreateCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IConfiguration config;

            public SubscriptionCreateCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IConfiguration config)
            {
                this.db = db;
                this.ctx = ctx;
                this.config = config;
            }

            async public Task<CommandJsonResponse> Handle(SubscriptionCreateCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    response.Error = true;
                    response.Message = "Email boş göndərilə bilməz!";
                }

                if (request.Email.IsEmail() == false)
                {
                    response.Error = true;
                    response.Message = "Xahiş olunur email formatında məlumat daxil edin!";
                }

                Subscription isSubscribed = await db.Subscriptions.FirstOrDefaultAsync(s => s.Email == request.Email, cancellationToken);

                if (isSubscribed != null)
                {
                    if (isSubscribed.IsConfirmed)
                    {
                        response.Error = true;
                        response.Message = "Siz artıq bizim abunəliyimizə qoşulmusunuz!";
                    }
                    else
                    {
                        response.Error = false;
                        response.Message = "Təsdiqə ehtiyac var, mail-inizə göndərilmiş linkə keçid edərək, mail-inizi təsdiq edin!";
                    }
                }

                string token = $"{request.Email}-{DateTime.UtcNow.AddHours(4):yyyyMMddHHmmss}";

                token = token.Encrypt();

                string url = $"{ctx.ActionContext.HttpContext.Request.Scheme}://{ctx.ActionContext.HttpContext.Request.Host}/subscribe-route/?token={token}";

                string fromMail = config.GetValue<string>("FactoryCredentials:Email");
                string pwd = config.GetValue<string>("FactoryCredentials:Password");
                string? cc = config.GetValue<string>("FactoryCredentials:CC");
                string subject = "Xahiş olunur abunəliyi təsdiq edin!";
                string body = $"Bu <a href='{url}'>linkə</a> klik edərək təsdiq pəncərəsinə yönələ bilərsiniz!";

                bool status = config.SendMail(fromMail, pwd, request.Email, subject, body, true, cc);

                if (!status)
                    response.Error = true;
                response.Message = "Təyin olunmayan xəta baş verdi, daha sonra yenidən yoxlayın!";

                Subscription data = new()
                {
                    Email = request.Email
                };

                await db.Subscriptions.AddAsync(data, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                response.Error = false;
                response.Message = "Mail-inizə göndərilmiş linkə keçid edərək, mail-inizi təsdiq edin!";

                return response;
            }
        }
    }
}
