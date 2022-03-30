using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.AppCode.Modules.MovieCommentModule
{
    public class MovieCommentCommand : IRequest<MovieComment>
    {
        [Required(ErrorMessage = "Bu hissə boş qoyula bilməz!")]
        public string Content { get; set; } = null!;

        public int? ParentId { get; set; }

        public int MovieId { get; set; }

        public int StarRating { get; set; }

        public bool IsSpoiler { get; set; }

        public class MovieCommentCommandHandler : IRequestHandler<MovieCommentCommand, MovieComment>
        {
            readonly MovieDbContext db;
            readonly IActionContextAccessor ctx;

            public MovieCommentCommandHandler(MovieDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            async public Task<MovieComment> Handle(MovieCommentCommand request, CancellationToken cancellationToken)
            {
                MovieComment movieComment = new();

                if ((request.ParentId ?? 0) > 0)
                {
                    movieComment.ParentId = request.ParentId;
                }

                movieComment.Content = request.Content;
                movieComment.MovieId = request.MovieId;
                movieComment.StarRating = request.StarRating;
                movieComment.IsSpoiler = request.IsSpoiler;
                movieComment.CreatedByUserId = ctx.GetUserId();

                await db.MovieComments.AddAsync(movieComment, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return movieComment;
            }
        }
    }
}
