using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.SubscriptionsModule
{
    public class SubscriptionGetAllActiveQuery : IRequest<IEnumerable<Subscription>>
    {
        public class SubscriptionGetAllActiveQueryHandler : IRequestHandler<SubscriptionGetAllActiveQuery, IEnumerable<Subscription>>
        {
            readonly MovieDbContext db;

            public SubscriptionGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Subscription>> Handle(SubscriptionGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Subscription> subscriptions = await db.Subscriptions.Where(g => g.DeletedDate == null).ToListAsync(cancellationToken);

                return subscriptions;
            }
        }
    }
}
