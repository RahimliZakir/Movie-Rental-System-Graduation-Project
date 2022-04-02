using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.MovieGenreCastItemModule
{
    public class MovieGenreCastItemCreateCommand : MovieGenreCastItemFormModel, IRequest<CommandJsonResponse>
    {
        public class MovieGenreCastItemCreateCommandHandler : IRequestHandler<MovieGenreCastItemCreateCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public MovieGenreCastItemCreateCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<CommandJsonResponse> Handle(MovieGenreCastItemCreateCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                try
                {
                    foreach (MovieGenreCastItem item in request.MovieGenreCastItems)
                    {
                        int userId = ctx.GetUserId();

                        MovieGenreCastItem data = new()
                        {
                            MovieId = request.Movie.Id,
                            GenreId = item.Genre.Id,
                            CastId = item.Cast.Id,
                            CreatedByUserId = userId
                        };

                        await db.MovieGenreCastItems.AddAsync(data, cancellationToken);
                    }

                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = "Uğurla əlavə olundu!";
                }
                catch (Exception ex)
                {
                    response.Error = true;
                    response.Message = "Məlumatlar əlavə olunan zaman xəta baş verdi!";
                }

                return response;
            }
        }
    }
}
