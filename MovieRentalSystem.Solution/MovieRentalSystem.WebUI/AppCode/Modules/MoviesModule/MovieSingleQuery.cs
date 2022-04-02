using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.MoviesModule
{
    public class MovieSingleQuery : IRequest<MovieViewModel>
    {
        public int? Id { get; set; }

        public class MovieSingleQueryHandler : IRequestHandler<MovieSingleQuery, MovieViewModel>
        {
            readonly MovieDbContext db;
            readonly IMapper mapper;

            public MovieSingleQueryHandler(MovieDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<MovieViewModel> Handle(MovieSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                Movie movie = await db.Movies
                                    .Include(s => s.Director)
                                    .Include(m => m.MovieComments)
                                    .ThenInclude(m => m.Children)
                                    .Include(m => m.MovieComments)
                                    .ThenInclude(m => m.CreatedByUser)
                                    .Include(m => m.MovieGenreCastItems)
                                    .ThenInclude(m => m.Genre)
                                    .Include(m => m.MovieGenreCastItems)
                                    .ThenInclude(m => m.Cast)
                                    .FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                MovieViewModel viewModel = mapper.Map<MovieViewModel>(movie);

                return viewModel;
            }
        }
    }
}
