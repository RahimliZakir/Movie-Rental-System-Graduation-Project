using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.DataContexts;

namespace MovieRentalSystem.WebUI.AppCode.Modules.MovieGenreCastItemModule
{
    public class MovieGenreCastItemSingleQuery : IRequest<MovieGenreCastItemFormModel>
    {
        public int? Id { get; set; }

        public class MovieGenreCastItemSingleQueryHandler : IRequestHandler<MovieGenreCastItemSingleQuery, MovieGenreCastItemFormModel>
        {
            readonly MovieDbContext db;

            public MovieGenreCastItemSingleQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<MovieGenreCastItemFormModel> Handle(MovieGenreCastItemSingleQuery request, CancellationToken cancellationToken)
            {
                MovieGenreCastItemFormModel fm = new();

                fm.Movie = await db.Movies.FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);

                fm.MovieGenreCastItems = await db.MovieGenreCastItems
                                                .Include(sgc => sgc.Genre)
                                                .Include(sgc => sgc.Cast)
                                                .Where(sgc => sgc.DeletedByUserId == null && sgc.MovieId.Equals(request.Id))
                                                .ToListAsync(cancellationToken);

                return fm;
            }
        }
    }
}
