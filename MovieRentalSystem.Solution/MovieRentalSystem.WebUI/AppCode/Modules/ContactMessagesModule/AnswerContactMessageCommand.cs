using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ContactMessagesModule
{
    public class AnswerContactMessageCommand : AnswerContactMessageFormModel, IRequest<CommandJsonResponse>
    {
        public class AnswerContactMessageCommandHandler : IRequestHandler<AnswerContactMessageCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IConfiguration conf;

            public AnswerContactMessageCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IConfiguration conf)
            {
                this.db = db;
                this.ctx = ctx;
                this.conf = conf;
            }

            async public Task<CommandJsonResponse> Handle(AnswerContactMessageCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = false;
                    response.Message = "Məlumat tamlığı qorunmayıb!";

                    goto end;
                }

                ContactMessage? entity = await db.ContactMessages.FirstOrDefaultAsync(c => c.Id == request.Id && c.DeletedDate == null, cancellationToken);

                if (!ctx.IsValid())
                {
                    response.Error = false;
                    response.Message = "Məlumatlar düzgün göndərilməyib!";

                    goto end;
                }

                if (ctx.IsValid())
                {
                    bool sentResponse = conf.SendMail(conf["FactoryCredentials:Email"], conf.GetValue<string>("FactoryCredentials:Password"), request.EmailAddress, "Cavab - Movie Rental Team", $"<h3>Sizin sual</h3> <p>- {request.Content}</p> <h3>Bizim cavab</h3> <p>- {request.Answer}</p>", true);

                    if (sentResponse == false)
                    {

                        response.Error = true;
                        response.Message = "Mail göndərilən zaman xəta baş verdi, biraz sonra yenidən yoxlayın!";

                        goto end;
                    }
                    else
                    {
                        try
                        {
                            entity.Answer = request.Answer;
                            entity.AnswerDate = DateTime.UtcNow.AddHours(4);
                            await db.SaveChangesAsync(cancellationToken);


                            response.Error = false;
                            response.Message = "Müraciət uğurla cavablandırıldı və istifadəçinin Email qutusuna göndərildi!";

                            goto end;
                        }
                        catch (Exception)
                        {
                            response.Error = false;
                            response.Message = "Xəta baş verdi, biraz sonra yenidən yoxlayın!";

                            goto end;
                        }
                    }
                }

            end:
                return response;
            }
        }
    }
}
