using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.MoviesModule
{
    public class MovieCreateCommand : MovieViewModel, IRequest<CommandJsonResponse>
    {
        public class MovieCreateCommandHandler : IRequestHandler<MovieCreateCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;

            public MovieCreateCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IMapper mapper)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
            }

            public async Task<CommandJsonResponse> Handle(MovieCreateCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (!ctx.IsValid())
                {
                    response.Error = true;
                    response.Message = "Məlumat düzgün göndərilməyib!";
                }

                if (ctx.IsValid())
                {
                    Movie movie = mapper.Map<Movie>(request);
                    movie.CreatedByUserId = ctx.GetUserId();

                    await db.Movies.AddAsync(movie, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = "Məlumat uğurla əlavə olundu!";
                }

            end:
                return response;
            }
        }
    }
}
