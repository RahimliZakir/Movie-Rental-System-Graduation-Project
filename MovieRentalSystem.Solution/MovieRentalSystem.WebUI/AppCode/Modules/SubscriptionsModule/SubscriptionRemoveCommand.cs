using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.SubscriptionsModule
{
    public class SubscriptionRemoveCommand : IRequest<CommandJsonResponse>
    {
        public int? Id { get; set; }

        public class SubscriptionRemoveCommandHandler : IRequestHandler<SubscriptionRemoveCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public SubscriptionRemoveCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<CommandJsonResponse> Handle(SubscriptionRemoveCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                if (request.Id == null)
                {
                    response.Error = true;
                    response.Message = "Məlumatın tamlığı qorunmayıb!";

                    goto end;
                }

                Subscription subscription = await db.Subscriptions.FirstOrDefaultAsync(g => g.Id == request.Id && g.DeletedDate == null, cancellationToken);

                if (subscription == null)
                {
                    response.Error = true;
                    response.Message = "Məlumat mövcud deyil!";

                    goto end;
                }

                subscription.DeletedDate = DateTime.UtcNow.AddHours(4);
                subscription.DeletedByUserId = ctx.GetUserId();
                await db.SaveChangesAsync(cancellationToken);

                response.Error = false;
                response.Message = "Məlumat uğurla silindi!";

            end:
                return response;
            }
        }
    }
}
