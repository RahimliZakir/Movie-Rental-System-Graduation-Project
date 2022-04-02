using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.MoviesModule
{
    public class MovieGetAllActiveQuery : IRequest<IEnumerable<Movie>>
    {
        public class MovieGetAllActiveQueryHandler : IRequestHandler<MovieGetAllActiveQuery, IEnumerable<Movie>>
        {
            readonly MovieDbContext db;

            public MovieGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Movie>> Handle(MovieGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Movie> movies = await db.Movies
                                                  .Include(s => s.Director)
                                                  .Include(m => m.MovieComments)
                                                  .ThenInclude(m => m.Children)
                                                  .Include(m => m.MovieComments)
                                                  .ThenInclude(m => m.CreatedByUser)
                                                  .Where(g => g.DeletedDate == null)
                                                  .ToListAsync(cancellationToken);

                return movies;
            }
        }
    }
}
