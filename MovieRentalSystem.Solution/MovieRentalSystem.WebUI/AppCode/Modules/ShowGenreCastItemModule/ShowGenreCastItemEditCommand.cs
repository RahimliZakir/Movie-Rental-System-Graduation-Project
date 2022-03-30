using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ShowGenreCastItemModule
{
    public class ShowGenreCastItemEditCommand : ShowGenreCastItemFormModel, IRequest<CommandJsonResponse>
    {
        public class ShowGenreCastItemEditCommandHandler : IRequestHandler<ShowGenreCastItemEditCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public ShowGenreCastItemEditCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<CommandJsonResponse> Handle(ShowGenreCastItemEditCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Show.Id == null || request.Show.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumatın tamlığı qorunmayıb!";
                    goto end;
                }

                if (request.ShowGenreCastItems == null)
                {
                    request.ShowGenreCastItems = new List<ShowGenreCastItem>();
                }

                var entity = await db.ShowGenreCastItems
                                   .Where(p => p.ShowId.Equals(request.Show.Id))
                                   .ToListAsync(cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə bir məlumat yoxdur!";
                    goto end;
                }

                foreach (var item in entity)
                {
                    var coming = request.ShowGenreCastItems.FirstOrDefault(b => b.Genre.Id == item.GenreId && b.Cast.Id == item.CastId);

                    if (coming == null)
                    {
                        var deleted = await db.ShowGenreCastItems
                                              .FirstOrDefaultAsync(b => b.ShowId == item.ShowId
                                              && b.GenreId == item.GenreId
                                              && b.CastId == item.CastId, cancellationToken);

                        deleted.DeletedByUserId = ctx.GetUserId();
                        deleted.DeletedDate = DateTime.UtcNow.AddHours(4);
                    }
                    else
                    {
                        item.ShowId = request.Show.Id;
                        item.GenreId = coming.Genre.Id;
                        item.CastId = coming.Cast.Id;
                    }
                }

                foreach (var item in request.ShowGenreCastItems.Where(b => !entity.Any(e => e.GenreId == b.Genre.Id && e.CastId == b.Cast.Id)))
                {
                    var collection = new ShowGenreCastItem
                    {
                        ShowId = request.Show.Id,
                        GenreId = item.Genre.Id,
                        CastId = item.Cast.Id,
                        CreatedByUserId = ctx.GetUserId()
                    };

                    await db.ShowGenreCastItems.AddAsync(collection, cancellationToken);
                }

                await db.SaveChangesAsync(cancellationToken);

                response.Error = false;
                response.Message = "Uğurla yeniləndi!";

            end:
                return response;
            }
        }
    }
}
