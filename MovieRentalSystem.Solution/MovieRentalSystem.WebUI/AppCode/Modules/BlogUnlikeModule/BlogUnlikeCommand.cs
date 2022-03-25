using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.BlogUnlikeModule
{
    public class BlogUnlikeCommand : IRequest<CommandJsonResponse>
    {
        public int BlogId { get; set; }

        public class BlogUnlikeCommandHandler : IRequestHandler<BlogUnlikeCommand, CommandJsonResponse>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public BlogUnlikeCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<CommandJsonResponse> Handle(BlogUnlikeCommand request, CancellationToken cancellationToken)
            {
                CommandJsonResponse response = new();

                int userId = ctx.GetUserId();

                if (await db.BlogLikes.AnyAsync(bl => bl.BlogId.Equals(request.BlogId) && bl.CreatedByUserId.Equals(userId), cancellationToken))
                {
                    BlogLike currentLikedBlog = await db.BlogLikes.FirstOrDefaultAsync(bl => bl.BlogId.Equals(request.BlogId) && bl.CreatedByUserId.Equals(userId), cancellationToken);

                    db.BlogLikes.Remove(currentLikedBlog);
                }

                if (!await db.BlogUnlikes.AnyAsync(bu => bu.BlogId.Equals(request.BlogId) && bu.CreatedByUserId.Equals(userId), cancellationToken))
                {
                    BlogUnlike unlikedBlog = new()
                    {
                        BlogId = request.BlogId,
                        CreatedByUserId = userId
                    };


                    await db.BlogUnlikes.AddAsync(unlikedBlog, cancellationToken);

                    response.Error = false;
                    response.LikeCount = await db.BlogLikes.CountAsync(bu => bu.BlogId == request.BlogId, cancellationToken);
                    response.UnlikeCount = await db.BlogUnlikes.CountAsync(bu => bu.BlogId == request.BlogId, cancellationToken);
                }

                await db.SaveChangesAsync(cancellationToken);

                return response;
            }
        }
    }
}
