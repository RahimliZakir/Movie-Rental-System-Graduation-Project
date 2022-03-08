using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.GenresModule
{
    public class GenreGetAllActiveQuery : IRequest<IEnumerable<Genre>>
    {
        public class GenreGetAllActiveQueryHandler : IRequestHandler<GenreGetAllActiveQuery, IEnumerable<Genre>>
        {
            readonly MovieDbContext db;

            public GenreGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Genre>> Handle(GenreGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Genre> genres = await db.Genres
                                                    .Include(g => g.Children.Where(g => g.DeletedDate == null))
                                                    .Where(g => g.DeletedDate == null)
                                                    .ToListAsync(cancellationToken);

                return genres;
            }
        }
    }
}
