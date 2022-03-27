using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.DirectorsModule
{
    public class DirectorGetAllActiveQuery : IRequest<IEnumerable<Director>>
    {
        public class DirectorGetAllActiveQueryHandler : IRequestHandler<DirectorGetAllActiveQuery, IEnumerable<Director>>
        {
            readonly MovieDbContext db;

            public DirectorGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Director>> Handle(DirectorGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Director> directors = await db.Directors.Where(g => g.DeletedDate == null).ToListAsync(cancellationToken);

                return directors;
            }
        }
    }
}
