using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.SubscriptionsModule
{
    public class SubscriptionSingleQuery : IRequest<SubscriptionViewModel>
    {
        public int? Id { get; set; }

        public string Email { get; set; }

        public SubscriptionSingleQuery(string email)
        {
            this.Email = email;
        }

        public SubscriptionSingleQuery() { }

        public class SubscriptionSingleQueryHandler : IRequestHandler<SubscriptionSingleQuery, SubscriptionViewModel>
        {
            readonly MovieDbContext db;
            readonly IMapper mapper;

            public SubscriptionSingleQueryHandler(MovieDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            async public Task<SubscriptionViewModel> Handle(SubscriptionSingleQuery request, CancellationToken cancellationToken)
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
                SubscriptionViewModel viewModel = mapper.Map<SubscriptionViewModel>(subscription);
                return viewModel;
            }
        }
    }
}
