using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.CastsModule
{
    public class CastGetAllActiveQuery : IRequest<IEnumerable<Cast>>
    {
        public class CastGetAllActiveQueryHandler : IRequestHandler<CastGetAllActiveQuery, IEnumerable<Cast>>
        {
            readonly MovieDbContext db;

            public CastGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Cast>> Handle(CastGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Cast> casts = await db.Casts.Where(g => g.DeletedDate == null).ToListAsync(cancellationToken);

                return casts;
            }
        }
    }
}
