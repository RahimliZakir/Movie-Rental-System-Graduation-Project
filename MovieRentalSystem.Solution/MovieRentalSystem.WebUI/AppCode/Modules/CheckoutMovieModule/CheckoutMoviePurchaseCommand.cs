using MediatR;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;

namespace MovieRentalSystem.WebUI.AppCode.Modules.CheckoutMovieModule
{
    public class CheckoutMoviePurchaseCommand : IRequest<CommandJsonResponse>
    {
        public class CheckoutMoviePurchaseCommandHandler : IRequestHandler<CheckoutMoviePurchaseCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;

            public CheckoutMoviePurchaseCommandHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<CommandJsonResponse> Handle(CheckoutMoviePurchaseCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
