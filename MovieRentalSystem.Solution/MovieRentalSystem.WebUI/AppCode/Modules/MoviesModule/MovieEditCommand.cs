using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.MoviesModule
{
    public class MovieEditCommand : MovieViewModel, IRequest<CommandJsonResponse>
    {
        public class MovieEditCommandHandler : IRequestHandler<MovieEditCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;
            readonly IHostEnvironment env;

            public MovieEditCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IMapper mapper, IHostEnvironment env)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
                this.env = env;
            }

            public async Task<CommandJsonResponse> Handle(MovieEditCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumatın tamlığı qorunmayıb!";
                    goto end;
                }

                Movie entity = await db.Movies.AsNoTracking().FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Məlumat mövcud deyil!";
                    goto end;
                }

                if (!ctx.IsValid())
                {
                    response.Error = true;
                    response.Message = "Məlumat düzgün göndərilməyib!";
                }

                if (ctx.IsValid())
                {
                    try
                    {
                        request.CreatedByUserId = entity.CreatedByUserId;
                        Movie movie = mapper.Map(request, entity);

                        db.Movies.Update(movie);
                        await db.SaveChangesAsync(cancellationToken);

                        response.Error = false;
                        response.Message = "Məlumat uğurla yeniləndi!";
                    }
                    catch (Exception ex)
                    {
                        response.Error = true;
                        response.Message = "Məlumat əlavə olunan zaman xəta baş verdi!";
                    }
                }

            end:
                return response;
            }
        }
    }
}
