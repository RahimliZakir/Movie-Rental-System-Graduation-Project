using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.SubscripitionsModule
{
    public class SubscriptionSingleQuery : IRequest<Subscription>
    {
        public int? Id { get; set; }

        public string Email { get; set; }

        public SubscriptionSingleQuery(string email)
        {
            this.Email = email;
        }

        public SubscriptionSingleQuery() { }

        public class SubscriptionSingleQueryHandler : IRequestHandler<SubscriptionSingleQuery, Subscription>
        {
            readonly MovieDbContext db;

            public SubscriptionSingleQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<Subscription> Handle(SubscriptionSingleQuery request, CancellationToken cancellationToken)
            {
                Subscription subscription = null;

                if (!string.IsNullOrWhiteSpace(request.Email))
                {
                    subscription = await db.Subscriptions.FirstOrDefaultAsync(s => s.Email == request.Email);
                    goto end;
                }

                if (request.Id == null)
                {
                    return null;
                }

                subscription = await db.Subscriptions.FirstOrDefaultAsync(s => s.Id == request.Id);

            end:
                return subscription;
            }
        }
    }
}
