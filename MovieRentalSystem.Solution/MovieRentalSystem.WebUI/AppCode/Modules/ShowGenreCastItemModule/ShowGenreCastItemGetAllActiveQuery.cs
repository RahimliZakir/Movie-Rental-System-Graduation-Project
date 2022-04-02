using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ShowGenreCastItemModule
{
    public class ShowGenreCastItemGetAllActiveQuery : IRequest<IEnumerable<Show>>
    {
        public class ShowGenreCastItemGetAllActiveQueryHandler : IRequestHandler<ShowGenreCastItemGetAllActiveQuery, IEnumerable<Show>>
        {
            readonly MovieDbContext db;

            public ShowGenreCastItemGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Show>> Handle(ShowGenreCastItemGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Show> shows = await db.Shows
                                                  .Include(s => s.ShowGenreCastItems.Where(sgc => sgc.DeletedByUserId == null))
                                                  .ThenInclude(s => s.Genre)
                                                  .Include(s => s.ShowGenreCastItems.Where(sgc => sgc.DeletedByUserId == null))
                                                  .ThenInclude(s => s.Cast)
                                                  .Where(s => s.ShowGenreCastItems.Any(sg => sg.DeletedByUserId == null) && s.ShowGenreCastItems.Any(sgc => sgc.ShowId == s.Id))
                                                  .ToListAsync(cancellationToken);

                return shows;
            }
        }
    }
}
