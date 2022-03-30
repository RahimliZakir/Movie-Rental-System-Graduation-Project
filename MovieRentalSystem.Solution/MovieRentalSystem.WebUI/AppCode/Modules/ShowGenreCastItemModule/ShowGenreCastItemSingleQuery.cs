using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.DataContexts;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ShowGenreCastItemModule
{
    public class ShowGenreCastItemSingleQuery : IRequest<ShowGenreCastItemFormModel>
    {
        public int? Id { get; set; }

        public class ShowGenreCastItemSingleQueryHandler : IRequestHandler<ShowGenreCastItemSingleQuery, ShowGenreCastItemFormModel>
        {
            readonly MovieDbContext db;

            public ShowGenreCastItemSingleQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<ShowGenreCastItemFormModel> Handle(ShowGenreCastItemSingleQuery request, CancellationToken cancellationToken)
            {
                ShowGenreCastItemFormModel fm = new();

                fm.Show = await db.Shows.FirstOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);

                fm.ShowGenreCastItems = await db.ShowGenreCastItems
                                                .Include(sgc => sgc.Genre)
                                                .Include(sgc => sgc.Cast)
                                                .Where(sgc => sgc.DeletedByUserId == null && sgc.ShowId.Equals(request.Id))
                                                .ToListAsync(cancellationToken);

                return fm;
            }
        }
    }
}
