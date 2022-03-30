using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;

namespace MovieRentalSystem.WebUI.AppCode.Modules.MovieCommentModule
{
    public class MovieCommentCountCommand : IRequest<int>
    {
        public int MovieId { get; set; }

        public class MovieCommentCountCommandHandler : IRequestHandler<MovieCommentCountCommand, int>
        {
            readonly MovieDbContext db;

            public MovieCommentCountCommandHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<int> Handle(MovieCommentCountCommand request, CancellationToken cancellationToken)
            {
                int count = await db.MovieComments
                                    .Where(bc => bc.ParentId == null && bc.MovieId == request.MovieId)
                                    .CountAsync(cancellationToken);

                return count;
            }
        }
    }
}
