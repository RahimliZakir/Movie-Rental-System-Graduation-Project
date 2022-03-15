using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.AppInfosModule
{
    public class AppInfoRemoveCommand : IRequest<CommandJsonResponse>
    {
        public int? Id { get; set; }

        public class AppInfoRemoveCommandHandler : IRequestHandler<AppInfoRemoveCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public AppInfoRemoveCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<CommandJsonResponse> Handle(AppInfoRemoveCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Id == null)
                {
                    response.Error = true;
                    response.Message = "Məlumatın tamlığı qorunmayıb!";

                    goto end;
                }

                AppInfo appInfo = await db.AppInfos.FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                if (appInfo == null)
                {
                    response.Error = true;
                    response.Message = "Məlumat mövcud deyil!";

                    goto end;
                }

                appInfo.DeletedDate = DateTime.UtcNow.AddHours(4);
                appInfo.DeletedByUserId = ctx.GetUserId();
                await db.SaveChangesAsync(cancellationToken);

                response.Error = false;
                response.Message = "Məlumat uğurla silindi!";

            end:
                return response;
            }
        }
    }
}
