using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.RoomsModule
{
    public class RoomGetAllActiveQuery : IRequest<IEnumerable<Room>>
    {
        public class RoomGetAllActiveQueryHandler : IRequestHandler<RoomGetAllActiveQuery, IEnumerable<Room>>
        {
            readonly MovieDbContext db;

            public RoomGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Room>> Handle(RoomGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Room> rooms = await db.Rooms.Where(g => g.DeletedDate == null).ToListAsync(cancellationToken);

                return rooms;
            }
        }
    }
}
