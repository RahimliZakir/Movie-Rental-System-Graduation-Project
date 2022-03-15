using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.GenresModule
{
    public class GenreRemoveCommand : IRequest<CommandJsonResponse>
    {
        public int? Id { get; set; }

        public class GenreRemoveCommandHandler : IRequestHandler<GenreRemoveCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public GenreRemoveCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<CommandJsonResponse> Handle(GenreRemoveCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Id == null)
                {
                    response.Error = true;
                    response.Message = "Məlumatın tamlığı qorunmayıb!";

                    goto end;
                }

                Genre genre = await db.Genres.FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                if (genre == null)
                {
                    response.Error = true;
                    response.Message = "Məlumat mövcud deyil!";

                    goto end;
                }

                genre.DeletedDate = DateTime.UtcNow.AddHours(4);
                genre.DeletedByUserId = ctx.GetUserId();
                await db.SaveChangesAsync(cancellationToken);

                response.Error = false;
                response.Message = "Məlumat uğurla silindi!";

            end:
                return response;
            }
        }
    }
}
