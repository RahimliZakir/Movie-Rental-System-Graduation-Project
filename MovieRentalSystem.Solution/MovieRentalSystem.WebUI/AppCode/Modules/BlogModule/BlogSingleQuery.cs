using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.BlogModule
{
    public class BlogSingleQuery : IRequest<BlogViewModel>
    {
        public int? Id { get; set; }

        public class BlogSingleQueryHandler : IRequestHandler<BlogSingleQuery, BlogViewModel>
        {
            readonly MovieDbContext db;
            readonly IMapper mapper;

            public BlogSingleQueryHandler(MovieDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            async public Task<BlogViewModel> Handle(BlogSingleQuery request, CancellationToken cancellationToken)
            {
                Blog blog = await db.Blogs
                                    .Include(b => b.BlogImages)
                                    .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedByUserId == null, cancellationToken);

                BlogViewModel vm = mapper.Map<BlogViewModel>(blog);

                return vm;
            }
        }
    }
}
