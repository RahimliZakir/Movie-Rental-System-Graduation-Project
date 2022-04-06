using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.ViewModels;

namespace MovieRentalSystem.WebUI.AppCode.Modules.CheckoutMovieModule
{
    public class CheckoutMoviePurchaseCommand : CheckoutViewModel, IRequest<CommandJsonResponse>
    {
        public class CheckoutMoviePurchaseCommandHandler : IRequestHandler<CheckoutMoviePurchaseCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public CheckoutMoviePurchaseCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<CommandJsonResponse> Handle(CheckoutMoviePurchaseCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                foreach (MovieCheckout item in request?.MovieCheckouts)
                {
                    item.ExpiredDate = DateTime.UtcNow.AddHours(4).AddDays(item.Period);
                    item.CreatedByUserId = ctx.GetUserId();

                    await db.MovieCheckouts.AddAsync(item, cancellationToken);
                }

                try
                {
                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = "Seçdiyiniz filmlər icarəyə götürüldü!";
                }
                catch (Exception ex)
                {
                    response.Error = true;
                    response.Message = "Xəta baş verdi, bir müddət sonra yenidən yoxlayın!";
                }

                return response;
            }
        }
    }
}
