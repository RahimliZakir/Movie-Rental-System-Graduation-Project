using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.RoomsModule
{
    public class RoomSingleQuery : IRequest<RoomViewModel>
    {
        public int? Id { get; set; }

        public class RoomSingleQueryHandler : IRequestHandler<RoomSingleQuery, RoomViewModel>
        {
            readonly MovieDbContext db;
            readonly IMapper mapper;

            public RoomSingleQueryHandler(MovieDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            public async Task<RoomViewModel> Handle(RoomSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return null;
                }

                Room room = await db.Rooms.FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                RoomViewModel viewModel = mapper.Map<RoomViewModel>(room);

                return viewModel;
            }
        }
    }
}
