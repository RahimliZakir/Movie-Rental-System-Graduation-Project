using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.AppInfosModule
{
    public class AppInfoSingleQuery : IRequest<AppInfoViewModel>
    {
        public int? Id { get; set; }

        public class AppInfoSingleQueryHandler : IRequestHandler<AppInfoSingleQuery, AppInfoViewModel>
        {
            readonly MovieDbContext db;
            readonly IMapper mapper;

            public AppInfoSingleQueryHandler(MovieDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<AppInfoViewModel> Handle(AppInfoSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                AppInfo appInfo = await db.AppInfos.FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                AppInfoViewModel viewModel = mapper.Map<AppInfoViewModel>(appInfo);

                return viewModel;
            }
        }
    }
}
