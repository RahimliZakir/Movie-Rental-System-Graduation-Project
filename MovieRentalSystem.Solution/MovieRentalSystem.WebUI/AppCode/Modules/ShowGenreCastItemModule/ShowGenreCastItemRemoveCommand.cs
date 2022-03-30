using MediatR;
using Microsoft.EntityFrameworkCore;
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

            public ShowGenreCastItemRemoveCommandHandler(MovieDbContext db)
            {
                this.db = db;
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
                                   .Where(pcc => pcc.ShowId.Equals(request.Id))
                                   .ToListAsync(cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə məlumat yoxudr!";
                    goto end;
                }

                foreach (var item in entity)
                {
                    db.ShowGenreCastItems.Remove(item);
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
