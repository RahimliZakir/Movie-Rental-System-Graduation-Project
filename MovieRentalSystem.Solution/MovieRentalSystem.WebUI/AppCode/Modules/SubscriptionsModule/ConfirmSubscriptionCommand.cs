using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.SubscriptionsModule
{
    public class ConfirmSubscriptionCommand : IRequest<CommandJsonResponse>
    {
        public string? Token { get; set; }

        public class ConfirmSubscriptionCommandHandler : IRequestHandler<ConfirmSubscriptionCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;

            public ConfirmSubscriptionCommandHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<CommandJsonResponse> Handle(ConfirmSubscriptionCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                string? token = request.Token;

                token = token?.Replace(" ", "+").Decrypt();

                string? email = token?.Split('-')[0];

                if (string.IsNullOrWhiteSpace(token))
                {
                    response.Error = true;
                    response.Message = "Xəta! Token yoxdur!";
                    goto end;
                }

                #region DateConversions
                string? expiredDate = token?.Split('-')[1];
                string? year = new(expiredDate?.Take(4).ToArray());
                string? month = expiredDate?.Substring(4, 2);
                string? day = expiredDate?.Substring(6, 2);
                string? hours = expiredDate?.Substring(8, 2);
                string? minutes = expiredDate?.Substring(10, 2);
                string? seconds = expiredDate?.Substring(12, 2);
                DateTime orderedExpiredDate = DateTime.ParseExact($"{day}.{month}.{year} {hours}:{minutes}:{seconds}", "dd.MM.yyyy HH:mm:ss", null);
                DateTime now = DateTime.UtcNow.AddHours(4);
                #endregion

                int compareResult = DateTime.Compare(orderedExpiredDate, now);

                Subscription subscribed = await db.Subscriptions.FirstOrDefaultAsync(s => s.Email.Equals(email), cancellationToken);

                if (compareResult <= 0)
                {
                    db.Subscriptions.Remove(subscribed);
                    await db.SaveChangesAsync(cancellationToken);

                    response.Error = true;
                    response.Message = "Token-in vaxtı bitmişdir, xahiş olunur yenidən abunə ol hissəsinə keçid edin!";
                    goto end;
                }

                if (subscribed == null)
                {
                    response.Error = true;
                    response.Message = "Belə bir mail abunəlik siyahısında yoxdur!";
                    goto end;
                }

                subscribed.IsConfirmed = true;
                subscribed.ConfirmationDate = DateTime.UtcNow.AddHours(4);

                await db.SaveChangesAsync(cancellationToken);

                response.Error = false;
                response.Message = "Müvəffəqiyyətlə abunə oldunuz!";

            end:
                return response;
            }
        }
    }
}
