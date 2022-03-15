using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.GenresModule
{
    public class GenreCreateCommand : GenreViewModel, IRequest<CommandJsonResponse>
    {
        public class GenreCreateCommandHandler : IRequestHandler<GenreCreateCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;

            public GenreCreateCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IMapper mapper)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
            }

            public async Task<CommandJsonResponse> Handle(GenreCreateCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (!ctx.IsValid())
                {
                    response.Error = true;
                    response.Message = "Məlumat düzgün göndərilməyib!";
                }

                if (ctx.IsValid())
                {
                    Genre genre = mapper.Map<Genre>(request);
                    genre.CreatedByUserId = ctx.GetUserId();

                    await db.Genres.AddAsync(genre, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = "Məlumat uğurla əlavə olundu!";
                }

                return response;
            }
        }
    }
}
