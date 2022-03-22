using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.AppCode.Modules.AccountModule
{
    public class SetClaimCommand : IRequest<CommandJsonResponse>
    {
        public int UserId { get; set; }
        public string Claim { get; set; }
        public bool Selected { get; set; }

        public class SetClaimCommandHandler : IRequestHandler<SetClaimCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;

            public SetClaimCommandHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<CommandJsonResponse> Handle(SetClaimCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                AppUser? user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
                if (user == null)
                {
                    response.Error = true;
                    response.Message = "Belə bir istifadəçi yoxdur!";
                    goto stopGenerate;
                }

                string[] policies = user.GetPolicies(typeof(SetClaimCommand));
                string? claim = policies.FirstOrDefault(u => u == request.Claim);
                if (claim == null)
                {
                    response.Error = true;
                    response.Message = "Belə bir claim yoxdur!";
                    goto stopGenerate;
                }

                AppUserClaim? userClaim = await db.UserClaims.FirstOrDefaultAsync(ur => ur.UserId == request.UserId && ur.ClaimType == request.Claim, cancellationToken);
                if (userClaim != null && request.Selected)
                {
                    response.Error = true;
                    response.Message = "Bu claim artıq təyin edilib!";
                    goto stopGenerate;
                }
                else if (userClaim == null && !request.Selected)
                {
                    response.Error = true;
                    response.Message = "Bu claim, bu istifadəçiyə aid deyil!";
                    goto stopGenerate;
                }

                if (request.Selected)
                {
                    AppUserClaim obj = new()
                    {
                        UserId = request.UserId,
                        ClaimType = request.Claim,
                        ClaimValue = "1"
                    };

                    await db.UserClaims.AddAsync(obj, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = $"{user.UserName} adlı istifadəçiyə {claim} claimi verildi!";
                }
                else
                {
                    db.UserClaims.Remove(userClaim);
                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = $"{user.UserName} adlı istifadəçidən {claim} claimi silindi!";
                }

            stopGenerate:
                return response;
            }
        }
    }
}
