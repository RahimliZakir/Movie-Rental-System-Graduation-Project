using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ShowsModule
{
    public class ShowGetAllActiveQuery : IRequest<IEnumerable<Show>>
    {
        public class ShowGetAllActiveQueryHandler : IRequestHandler<ShowGetAllActiveQuery, IEnumerable<Show>>
        {
            readonly MovieDbContext db;

            public ShowGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Show>> Handle(ShowGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Show> shows = await db.Shows
                                                  .Include(s => s.Director)
                                                  .Include(s => s.ShowComments)
                                                  .ThenInclude(s => s.Children)
                                                  .Include(s => s.ShowComments)
                                                  .ThenInclude(s => s.CreatedByUser)
                                                  .Where(g => g.DeletedDate == null)
                                                  .ToListAsync(cancellationToken);

                return shows;
            }
        }
    }
}
