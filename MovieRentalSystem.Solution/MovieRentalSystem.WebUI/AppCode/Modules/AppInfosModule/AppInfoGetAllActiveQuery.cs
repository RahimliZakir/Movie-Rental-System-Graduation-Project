using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.AppInfosModule
{
    public class AppInfoGetAllActiveQuery : IRequest<IEnumerable<AppInfo>>
    {
        public class AppInfoGetAllActiveQueryHandler : IRequestHandler<AppInfoGetAllActiveQuery, IEnumerable<AppInfo>>
        {
            readonly MovieDbContext db;

            public AppInfoGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<AppInfo>> Handle(AppInfoGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<AppInfo> appInfos = await db.AppInfos.Where(g => g.DeletedDate == null).ToListAsync(cancellationToken);

                return appInfos;
            }
        }
    }
}
