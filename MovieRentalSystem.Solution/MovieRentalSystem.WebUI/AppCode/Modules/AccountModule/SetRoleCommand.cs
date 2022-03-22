using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.AppCode.Modules.AccountModule
{
    public class SetRoleCommand : IRequest<CommandJsonResponse>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool Selected { get; set; }

        public class SetRoleCommandHandler : IRequestHandler<SetRoleCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;

            public SetRoleCommandHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<CommandJsonResponse> Handle(SetRoleCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                AppUser? user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
                if (user == null)
                {
                    response.Error = true;
                    response.Message = "Belə bir istifadəçi yoxdur!";
                    goto stopGenerate;
                }

                AppRole? role = await db.Roles.FirstOrDefaultAsync(u => u.Id == request.RoleId, cancellationToken);
                if (role == null)
                {
                    response.Error = true;
                    response.Message = "Belə bir rol yoxdur!";
                    goto stopGenerate;
                }

                AppUserRole? userRole = await db.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == request.UserId && ur.RoleId == request.RoleId, cancellationToken);
                if (userRole != null && request.Selected)
                {
                    response.Error = true;
                    response.Message = "Bu rol artıq təyin edilib!";
                    goto stopGenerate;
                }
                else if (userRole == null && !request.Selected)
                {
                    response.Error = true;
                    response.Message = "Bu rol, bu istifadəçiyə aid deyil!";
                    goto stopGenerate;
                }

                if (request.Selected)
                {
                    AppUserRole obj = new()
                    {
                        UserId = request.UserId,
                        RoleId = request.RoleId
                    };

                    await db.UserRoles.AddAsync(obj, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = $"{user.UserName} adlı istifadəçiyə {role.Name} rolu verildi!";
                }
                else
                {
                    db.UserRoles.Remove(userRole);
                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = $"{user.UserName} adlı istifadəçidən {role.Name} rolu silindi!";
                }

            stopGenerate:
                return response;
            }
        }
    }
}
