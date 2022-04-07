using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ShowsModule
{
    public class ShowSingleQuery : IRequest<ShowViewModel>
    {
        public int? Id { get; set; }

        public class ShowSingleQueryHandler : IRequestHandler<ShowSingleQuery, ShowViewModel>
        {
            readonly MovieDbContext db;
            readonly IMapper mapper;

            public ShowSingleQueryHandler(MovieDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<ShowViewModel> Handle(ShowSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                Show show = await db.Shows
                                    .Include(s => s.Director)
                                    .Include(s => s.Room)
                                    .ThenInclude(s => s.Seats)
                                    .Include(s => s.ShowComments)
                                    .ThenInclude(s => s.Children)
                                    .Include(s => s.ShowComments)
                                    .ThenInclude(s => s.CreatedByUser)
                                    .Include(s => s.ShowGenreCastItems)
                                    .ThenInclude(s => s.Genre)
                                    .Include(s => s.ShowGenreCastItems)
                                    .ThenInclude(s => s.Cast)
                                    .FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                ShowViewModel viewModel = mapper.Map<ShowViewModel>(show);

                return viewModel;
            }
        }
    }
}
