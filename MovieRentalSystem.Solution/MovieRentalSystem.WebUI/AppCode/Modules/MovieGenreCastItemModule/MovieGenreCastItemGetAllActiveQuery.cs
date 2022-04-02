using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.MovieGenreCastItemModule
{
    public class MovieGenreCastItemGetAllActiveQuery : IRequest<IEnumerable<Movie>>
    {
        public class MovieGenreCastItemGetAllActiveQueryHandler : IRequestHandler<MovieGenreCastItemGetAllActiveQuery, IEnumerable<Movie>>
        {
            readonly MovieDbContext db;

            public MovieGenreCastItemGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Movie>> Handle(MovieGenreCastItemGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Movie> movies = await db.Movies
                                                  .Include(s => s.MovieGenreCastItems.Where(sgc => sgc.DeletedByUserId == null))
                                                  .ThenInclude(s => s.Genre)
                                                  .Include(s => s.MovieGenreCastItems.Where(sgc => sgc.DeletedByUserId == null))
                                                  .ThenInclude(s => s.Cast)
                                                  .Where(s => s.MovieGenreCastItems.Any(sg => sg.DeletedByUserId == null) && s.MovieGenreCastItems.Any(sgc => sgc.MovieId == s.Id))
                                                  .ToListAsync(cancellationToken);

                return movies;
            }
        }
    }
}
