using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.AutoMapper.Dtos;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.AppCode.Modules.PersonalSideModule
{
    public class LayoutUserQuery : IRequest<LayoutUserDto>
    {
        public class LayoutUserQueryHandler : IRequestHandler<LayoutUserQuery, LayoutUserDto>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;

            public LayoutUserQueryHandler(MovieDbContext db, IActionContextAccessor ctx, IMapper mapper)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
            }

            async public Task<LayoutUserDto> Handle(LayoutUserQuery request, CancellationToken cancellationToken)
            {
                int userId = ctx.GetUserId();

                AppUserRole currentUserRole = await db.UserRoles
                                              .Include(u => u.User)
                                              .Include(u => u.Role)
                                              .FirstOrDefaultAsync(u => u.UserId == userId);

                LayoutUserDto userDto = mapper.Map<LayoutUserDto>(currentUserRole);

                return userDto;
            }
        }
    }
}
