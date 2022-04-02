using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ShowGenreCastItemModule
{
    public class ShowGenreCastItemRemoveCommand : IRequest<CommandJsonResponse>
    {
        public int? Id { get; set; }

        public class ShowGenreCastItemRemoveCommandHandler : IRequestHandler<ShowGenreCastItemRemoveCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public ShowGenreCastItemRemoveCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<CommandJsonResponse> Handle(ShowGenreCastItemRemoveCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumat tamlığı qorunmayıb!";
                    goto end;
                }

                var entity = await db.ShowGenreCastItems
                                   .Where(pcc => pcc.DeletedByUserId == null && pcc.ShowId.Equals(request.Id))
                                   .ToListAsync(cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə məlumat yoxudr!";
                    goto end;
                }

                foreach (var item in entity)
                {
                    item.DeletedByUserId = ctx.GetUserId();
                    item.DeletedDate = DateTime.UtcNow.AddHours(4);
                }

                await db.SaveChangesAsync(cancellationToken);

                response.Error = false;
                response.Message = "Seçdiyiniz məlumat uğurla silindi!";

            end:
                return response;
            }
        }
    }
}
