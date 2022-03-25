using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.AppCode.Modules.BlogCommentModule
{
    public class BlogCommentCommand : IRequest<BlogComment>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Content { get; set; } = null!;

        public int? ParentId { get; set; }

        public int BlogId { get; set; }

        public class BlogCommentCommandHandler : IRequestHandler<BlogCommentCommand, BlogComment>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public BlogCommentCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<BlogComment> Handle(BlogCommentCommand request, CancellationToken cancellationToken)
            {
                BlogComment blogComment = new();

                if ((request.ParentId ?? 0) > 0)
                {
                    blogComment.ParentId = request.ParentId;
                }

                blogComment.Content = request.Content;
                blogComment.BlogId = request.BlogId;
                blogComment.CreatedByUserId = ctx.GetUserId();

                await db.BlogComments.AddAsync(blogComment, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return blogComment;
            }
        }
    }
}
