using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ShowCommentModule
{
    public class ShowCommentCommand : IRequest<ShowComment>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Content { get; set; } = null!;

        public int? ParentId { get; set; }

        public int ShowId { get; set; }

        public int StarRating { get; set; }

        public bool IsSpoiler { get; set; }

        public class ShowCommentCommandHandler : IRequestHandler<ShowCommentCommand, ShowComment>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public ShowCommentCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<ShowComment> Handle(ShowCommentCommand request, CancellationToken cancellationToken)
            {
                ShowComment showComment = new();

                if ((request.ParentId ?? 0) > 0)
                {
                    showComment.ParentId = request.ParentId;
                }

                showComment.Content = request.Content;
                showComment.ShowId = request.ShowId;
                showComment.StarRating = request.StarRating;
                showComment.IsSpoiler = request.IsSpoiler;
                showComment.CreatedByUserId = ctx.GetUserId();

                await db.ShowComments.AddAsync(showComment, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return showComment;
            }
        }
    }
}
