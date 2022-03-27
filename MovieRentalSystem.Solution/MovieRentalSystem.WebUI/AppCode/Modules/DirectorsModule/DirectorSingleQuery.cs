using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.DirectorsModule
{
    public class DirectorSingleQuery : IRequest<DirectorViewModel>
    {
        public int? Id { get; set; }

        public class DirectorSingleQueryHandler : IRequestHandler<DirectorSingleQuery, DirectorViewModel>
        {
            readonly MovieDbContext db;
            readonly IMapper mapper;

            public DirectorSingleQueryHandler(MovieDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<DirectorViewModel> Handle(DirectorSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                Director director = await db.Directors.FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                DirectorViewModel viewModel = mapper.Map<DirectorViewModel>(director);

                return viewModel;
            }
        }
    }
}
