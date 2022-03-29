using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ShowCommentModule
{
    public class ShowCommentCountCommand : IRequest<int>
    {
        public int ShowId { get; set; }

        public class ShowCommentCountCommandHandler : IRequestHandler<ShowCommentCountCommand, int>
        {
            readonly MovieDbContext db;

            public ShowCommentCountCommandHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<int> Handle(ShowCommentCountCommand request, CancellationToken cancellationToken)
            {
                int count = await db.ShowComments
                                    .Where(bc => bc.ParentId == null && bc.ShowId == request.ShowId)
                                    .CountAsync(cancellationToken);

                return count;
            }
        }
    }
}
