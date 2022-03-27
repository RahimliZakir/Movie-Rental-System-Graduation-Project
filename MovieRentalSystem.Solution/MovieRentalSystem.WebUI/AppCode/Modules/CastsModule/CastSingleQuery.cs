using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.CastsModule
{
    public class CastSingleQuery : IRequest<CastViewModel>
    {
        public int? Id { get; set; }

        public class CastSingleQueryHandler : IRequestHandler<CastSingleQuery, CastViewModel>
        {
            readonly MovieDbContext db;
            readonly IMapper mapper;

            public CastSingleQueryHandler(MovieDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<CastViewModel> Handle(CastSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                Cast cast = await db.Casts.FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                CastViewModel viewModel = mapper.Map<CastViewModel>(cast);

                return viewModel;
            }
        }
    }
}
