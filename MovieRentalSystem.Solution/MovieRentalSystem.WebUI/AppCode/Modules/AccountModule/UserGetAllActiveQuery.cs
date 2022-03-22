using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.AppCode.Modules.AccountModule
{
    public class UserGetAllActiveQuery : IRequest<IEnumerable<AppUser>>
    {
        public class UserGetAllActiveQueryHandler : IRequestHandler<UserGetAllActiveQuery, IEnumerable<AppUser>>
        {
            readonly MovieDbContext db;

            public UserGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<AppUser>> Handle(UserGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                return await db.Users.ToListAsync(cancellationToken);
            }
        }
    }
}
