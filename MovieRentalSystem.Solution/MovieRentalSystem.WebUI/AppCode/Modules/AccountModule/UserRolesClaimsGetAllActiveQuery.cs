using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.Areas.Admin.Models.ViewModels;
using MovieRentalSystem.WebUI.Models.DataContexts;

namespace MovieRentalSystem.WebUI.AppCode.Modules.AccountModule
{
    public class UserRolesClaimsGetAllActiveQuery : IRequest<UserPolicyViewModel>
    {
        public int Id { get; set; }

        public class UserRolesClaimsGetAllActiveQueryHandler : IRequestHandler<UserRolesClaimsGetAllActiveQuery, UserPolicyViewModel>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IServiceProvider provider;
            public UserRolesClaimsGetAllActiveQueryHandler(MovieDbContext db, IActionContextAccessor ctx, IServiceProvider provider)
            {
                this.db = db;
                this.ctx = ctx;
                this.provider = provider;
            }

            async public Task<UserPolicyViewModel> Handle(UserRolesClaimsGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                UserPolicyViewModel vm = new();

                int userId = request.Id;

                vm.User = await db.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

                vm.Roles = await (from r in db.Roles
                                  join ur in db.UserRoles.Where(_ => _.UserId == userId)
                                  on r.Id equals ur.RoleId into urr
                                  from urr_item in urr.DefaultIfEmpty()
                                  select Tuple.Create(r, urr_item != null)).ToListAsync(cancellationToken);



                string[] policies = ctx.GetPolicies(typeof(UserRolesClaimsGetAllActiveQuery));

                vm.Claims = (from c in policies
                             join uc in db.UserClaims.Where(_ => _.UserId == userId && _.ClaimValue.Equals("1"))
                             on c equals uc.ClaimType into ucJoined
                             from ucJoined_item in ucJoined.DefaultIfEmpty()
                             select Tuple.Create(c, ucJoined_item != null)).ToList();

                return vm;
            }
        }
    }
}
