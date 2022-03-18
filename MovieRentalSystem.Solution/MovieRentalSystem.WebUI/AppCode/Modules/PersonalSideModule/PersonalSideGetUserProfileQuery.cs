using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.AppCode.Modules.PersonalSideModule
{
    public class PersonalSideGetUserProfileQuery : IRequest<PersonalSideFormModel>
    {
        public class PersonalSideGetUserProfileQueryHandler : IRequestHandler<PersonalSideGetUserProfileQuery, PersonalSideFormModel>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;

            public PersonalSideGetUserProfileQueryHandler(MovieDbContext db, IActionContextAccessor ctx, IMapper mapper)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
            }

            public async Task<PersonalSideFormModel> Handle(PersonalSideGetUserProfileQuery request, CancellationToken cancellationToken)
            {
                int userId = ctx.GetUserId();

                AppUser currentUser = await db.Users.FindAsync(userId);

                PersonalSideFormModel formModel = mapper.Map<PersonalSideFormModel>(currentUser);

                return formModel;
            }
        }
    }
}
