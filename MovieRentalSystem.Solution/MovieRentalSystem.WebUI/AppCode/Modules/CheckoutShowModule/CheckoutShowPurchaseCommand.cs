using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.ViewModels;

namespace MovieRentalSystem.WebUI.AppCode.Modules.CheckoutShowModule
{
    public class CheckoutShowPurchaseCommand : CheckoutViewModel, IRequest<CommandJsonResponse>
    {
        public class CheckoutShowPurchaseCommandHandler : IRequestHandler<CheckoutShowPurchaseCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public CheckoutShowPurchaseCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<CommandJsonResponse> Handle(CheckoutShowPurchaseCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                foreach (ShowCheckout item in request?.ShowCheckouts)
                {
                    item.ExpiredDate = DateTime.UtcNow.AddHours(4).AddDays(5);
                    item.CreatedByUserId = ctx.GetUserId();

                    await db.ShowCheckouts.AddAsync(item, cancellationToken);
                }

                try
                {
                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = false;
                    response.Message = "Seçdiyiniz tamaşaya bilet alındı!";
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
