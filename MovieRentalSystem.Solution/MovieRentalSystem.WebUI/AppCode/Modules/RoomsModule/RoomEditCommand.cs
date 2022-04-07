using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.RoomsModule
{
    public class RoomEditCommand : RoomViewModel, IRequest<CommandJsonResponse>
    {
        public class RoomEditCommandHandler : IRequestHandler<RoomEditCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;

            public RoomEditCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IMapper mapper)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
            }

            public async Task<CommandJsonResponse> Handle(RoomEditCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumatın tamlığı qorunmayıb!";
                    goto end;
                }

                Room entity = await db.Rooms.AsNoTracking()
                                            .Include(r => r.Seats)
                                            .FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Məlumat mövcud deyil!";
                    goto end;
                }

                if (!ctx.IsValid())
                {
                    response.Error = true;
                    response.Message = "Məlumat düzgün göndərilməyib!";
                }

                if (ctx.IsValid())
                {
                    int userId = ctx.GetUserId();

                    try
                    {
                        if (request.SeatCount != entity.Seats.Count)
                        {
                            foreach (var item in entity.Seats)
                            {
                                db.Seats.Remove(item);
                            }
                            for (int i = 0; i < request.SeatCount; i++)
                            {
                                var seat = new Seat
                                {
                                    CreatedByUserId = userId,
                                    RoomId = (int)request.Id
                                };
                                await db.Seats.AddAsync(seat, cancellationToken);
                            }
                        }

                        request.CreatedByUserId = entity.CreatedByUserId;
                        Room room = mapper.Map(request, entity);

                        await db.SaveChangesAsync(cancellationToken);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    response.Error = false;
                    response.Message = "Məlumat uğurla yeniləndi!";
                }

            end:
                return response;
            }
        }
    }
}
