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

                AppUser currentUser = await db.Users
                                              .Include(u => u.UserRoles)
                                              .ThenInclude(u => u.Role)
                                              .FirstOrDefaultAsync(u => u.Id == userId);

                LayoutUserDto userDto = mapper.Map<LayoutUserDto>(currentUser);

                return userDto;
            }
        }
    }
}
