using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.MovieGenreCastItemModule
{
    public class MovieGenreCastItemEditCommand : MovieGenreCastItemFormModel, IRequest<CommandJsonResponse>
    {
        public class MovieGenreCastItemEditCommandHandler : IRequestHandler<MovieGenreCastItemEditCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public MovieGenreCastItemEditCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<CommandJsonResponse> Handle(MovieGenreCastItemEditCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Movie.Id == null || request.Movie.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumatın tamlığı qorunmayıb!";
                    goto end;
                }

                if (request.MovieGenreCastItems == null)
                {
                    request.MovieGenreCastItems = new List<MovieGenreCastItem>();
                }

                var entity = await db.MovieGenreCastItems
                                   .Where(p => p.MovieId.Equals(request.Movie.Id) && p.DeletedByUserId == null)
                                   .ToListAsync(cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə bir məlumat yoxdur!";
                    goto end;
                }

                foreach (var item in entity)
                {
                    var coming = request.MovieGenreCastItems.FirstOrDefault(b => b.Genre.Id == item.GenreId && b.Cast.Id == item.CastId);

                    if (coming == null)
                    {
                        var deleted = await db.ShowGenreCastItems
                                              .FirstOrDefaultAsync(b => b.ShowId == item.MovieId
                                              && b.GenreId == item.GenreId
                                              && b.CastId == item.CastId && b.DeletedByUserId == null, cancellationToken);

                        deleted.DeletedByUserId = ctx.GetUserId();
                        deleted.DeletedDate = DateTime.UtcNow.AddHours(4);
                    }
                    else
                    {
                        item.MovieId = request.Movie.Id;
                        item.GenreId = coming.Genre.Id;
                        item.CastId = coming.Cast.Id;
                    }
                }

                foreach (var item in request.MovieGenreCastItems.Where(b => !entity.Any(e => e.GenreId == b.Genre.Id && e.CastId == b.Cast.Id)))
                {
                    var collection = new MovieGenreCastItem
                    {
                        MovieId = request.Movie.Id,
                        GenreId = item.Genre.Id,
                        CastId = item.Cast.Id,
                        CreatedByUserId = ctx.GetUserId()
                    };

                    await db.MovieGenreCastItems.AddAsync(collection, cancellationToken);
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
