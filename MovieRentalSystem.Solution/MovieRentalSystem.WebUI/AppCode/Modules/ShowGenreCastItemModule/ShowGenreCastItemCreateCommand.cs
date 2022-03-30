using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ShowGenreCastItemModule
{
    public class ShowGenreCastItemCreateCommand : ShowGenreCastItemFormModel, IRequest<CommandJsonResponse>
    {
        public class ShowGenreCastItemCreateCommandHandler : IRequestHandler<ShowGenreCastItemCreateCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public ShowGenreCastItemCreateCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<CommandJsonResponse> Handle(ShowGenreCastItemCreateCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                try
                {
                    foreach (ShowGenreCastItem item in request.ShowGenreCastItems)
                    {
                        ShowGenreCastItem data = new()
                        {
                            ShowId = request.Show.Id,
                            GenreId = item.Genre.Id,
                            CastId = item.Cast.Id
                        };

                        await db.ShowGenreCastItems.AddAsync(data, cancellationToken);
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
