using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.FaqsModule
{
    public class FaqGetAllActiveQuery : IRequest<IEnumerable<Faq>>
    {
        public class FaqGetAllActiveQueryHandler : IRequestHandler<FaqGetAllActiveQuery, IEnumerable<Faq>>
        {
            readonly MovieDbContext db;

            public FaqGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Faq>> Handle(FaqGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Faq> faqs = await db.Faqs.Where(g => g.DeletedDate == null).ToListAsync(cancellationToken);

                return faqs;
            }
        }
    }
}
