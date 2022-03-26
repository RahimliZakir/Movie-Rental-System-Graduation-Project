using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;

namespace MovieRentalSystem.WebUI.AppCode.Modules.BlogCommentModule
{
    public class BlogCommentCountCommand : IRequest<int>
    {
        public int BlogId { get; set; }

        public class BlogCommentCountCommandHandler : IRequestHandler<BlogCommentCountCommand, int>
        {
            readonly MovieDbContext db;

            public BlogCommentCountCommandHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<int> Handle(BlogCommentCountCommand request, CancellationToken cancellationToken)
            {
                int count = await db.BlogComments
                                    .Where(bc => bc.ParentId == null && bc.BlogId == request.BlogId)
                                    .CountAsync(cancellationToken);

                return count;
            }
        }
    }
}
