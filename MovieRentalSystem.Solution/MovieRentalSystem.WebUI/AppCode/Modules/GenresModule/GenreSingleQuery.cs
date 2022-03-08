using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.GenresModule
{
    public class GenreSingleQuery : IRequest<GenreViewModel>
    {
        public int? Id { get; set; }

        public class GenreSingleQueryHandler : IRequestHandler<GenreSingleQuery, GenreViewModel>
        {
            readonly MovieDbContext db;
            readonly IMapper mapper;

            public GenreSingleQueryHandler(MovieDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<GenreViewModel> Handle(GenreSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                Genre genre = await db.Genres
                                      .Include(g => g.Parent)
                                      .FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                GenreViewModel viewModel = mapper.Map<GenreViewModel>(genre);

                return viewModel;
            }
        }
    }
}
