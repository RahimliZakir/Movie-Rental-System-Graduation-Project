using MediatR.Pipeline;
using MovieRentalSystem.WebUI.Models.DataContexts;

namespace MovieRentalSystem.WebUI.AppCode.Behaviours
{
    public class PreRequestBehaviour<T> : IRequestPreProcessor<T>
        where T : notnull
    {
        readonly MovieDbContext db;

        public PreRequestBehaviour(MovieDbContext db)
        {
            this.db = db;
        }

        async public Task Process(T request, CancellationToken cancellationToken)
        {
            if (db.Database.CurrentTransaction != null)
                await db.Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
