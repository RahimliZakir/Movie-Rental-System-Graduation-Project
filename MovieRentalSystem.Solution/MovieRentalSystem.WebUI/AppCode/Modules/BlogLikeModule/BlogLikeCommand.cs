using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.BlogLikeModule
{
    public class BlogLikeCommand : IRequest<CommandJsonResponse>
    {
        public int BlogId { get; set; }

        public class BlogLikeCommandHandler : IRequestHandler<BlogLikeCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public BlogLikeCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<CommandJsonResponse> Handle(BlogLikeCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                int userId = ctx.GetUserId();

                if (await db.BlogUnlikes.AnyAsync(bu => bu.BlogId.Equals(request.BlogId) && bu.CreatedByUserId.Equals(userId), cancellationToken))
                {
                    BlogUnlike currentUnlikedBlog = await db.BlogUnlikes.FirstOrDefaultAsync(bl => bl.BlogId.Equals(request.BlogId) && bl.CreatedByUserId.Equals(userId), cancellationToken);

                    db.BlogUnlikes.Remove(currentUnlikedBlog);
                }

                if (!await db.BlogLikes.AnyAsync(bl => bl.BlogId.Equals(request.BlogId) && bl.CreatedByUserId.Equals(userId), cancellationToken))
                {
                    BlogLike likedBlog = new()
                    {
                        BlogId = request.BlogId,
                        CreatedByUserId = userId
                    };

                    await db.BlogLikes.AddAsync(likedBlog, cancellationToken);

                    response.Error = false;
                    response.UnlikeCount = await db.BlogUnlikes.CountAsync(bu => bu.BlogId == request.BlogId, cancellationToken);
                    response.LikeCount = await db.BlogLikes.CountAsync(bu => bu.BlogId == request.BlogId, cancellationToken);
                }

                await db.SaveChangesAsync(cancellationToken);

                return response;
            }
        }
    }
}
