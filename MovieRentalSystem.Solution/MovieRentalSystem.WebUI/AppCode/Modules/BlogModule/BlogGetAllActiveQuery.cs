using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.BlogModule
{
    public class BlogGetAllActiveQuery : IRequest<IEnumerable<Blog>>
    {
        public class BlogGetAllActiveQueryHandler : IRequestHandler<BlogGetAllActiveQuery, IEnumerable<Blog>>
        {
            readonly MovieDbContext db;

            public BlogGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Blog>> Handle(BlogGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Blog> blogs = await db.Blogs
                                                  .Include(b => b.CreatedByUser)
                                                  .Include(b => b.BlogImages)
                                                  .Include(b => b.BlogLikes)
                                                  .Include(b => b.BlogUnlikes)
                                                  .Include(b => b.BlogComments)
                                                  .ThenInclude(b => b.Children)
                                                  .Include(b => b.BlogComments)
                                                  .ThenInclude(b => b.CreatedByUser)
                                                  .Where(b => b.DeletedByUserId == null)
                                                  .ToListAsync(cancellationToken);

                return blogs;
            }
        }
    }
}
