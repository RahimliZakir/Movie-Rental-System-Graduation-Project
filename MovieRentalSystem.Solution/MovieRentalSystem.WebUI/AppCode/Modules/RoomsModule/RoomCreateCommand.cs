﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.RoomsModule
{
    public class RoomCreateCommand : RoomViewModel, IRequest<CommandJsonResponse>
    {
        public class RoomCreateCommandHandler : IRequestHandler<RoomCreateCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;

            public RoomCreateCommandHandler(MovieDbContext db, IActionContextAccessor ctx, IMapper mapper)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
            }

            public async Task<CommandJsonResponse> Handle(RoomCreateCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (!ctx.IsValid())
                {
                    response.Error = true;
                    response.Message = "Məlumat düzgün göndərilməyib!";
                }

                if (ctx.IsValid())
                {
                    Room room = mapper.Map<Room>(request);
                    room.CreatedByUserId = ctx.GetUserId();

                    await db.Rooms.AddAsync(room, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = "Məlumat uğurla əlavə olundu!";
                }

                return response;
            }
        }
    }
}
