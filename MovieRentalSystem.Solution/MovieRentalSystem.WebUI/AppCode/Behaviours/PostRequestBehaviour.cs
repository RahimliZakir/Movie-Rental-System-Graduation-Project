using MediatR;
using MediatR.Pipeline;
using MovieRentalSystem.WebUI.Models.DataContexts;

namespace MovieRentalSystem.WebUI.AppCode.Behaviours
{
    public class PostRequestBehaviour<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        readonly MovieDbContext db;

        public PostRequestBehaviour(MovieDbContext db)
        {
            this.db = db;
        }

        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            if (db.Database.CurrentTransaction != null)
            {
                await db.Database.CurrentTransaction.CommitAsync(cancellationToken);
            }
        }
    }
}
