using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.ViewModels;

namespace MovieRentalSystem.WebUI.AppCode.Modules.BlogModule
{
    public class BlogPagedQuery : IRequest<PagedViewModel<Blog>>
    {
        int pageIndex, pageSize;

        #region PaginationSettings
        public int PageIndex
        {
            get
            {
                if (pageIndex > 0)
                    return pageIndex;

                return 1;
            }
            set
            {
                if (value > 0)
                    pageIndex = value;
                else
                {
                    pageIndex = 1;
                }
            }
        }

        public int PageSize
        {
            get
            {
                if (pageSize > 0)
                    return pageSize;

                return 10;
            }
            set
            {
                if (value > 0)
                    pageSize = value;
                else
                {
                    pageSize = 10;
                }
            }
        }

        public BlogPagedQuery() { }

        public BlogPagedQuery(int pageIndex = 1, int pageSize = 10)
        {
            this.PageIndex = pageIndex;
            this.pageSize = pageSize;
        }
        #endregion

        public PagedViewModel<Blog> Response { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public class BlogPagedQueryHandler : IRequestHandler<BlogPagedQuery, PagedViewModel<Blog>>
        {
            readonly MovieDbContext db;

            public BlogPagedQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<PagedViewModel<Blog>> Handle(BlogPagedQuery request, CancellationToken cancellationToken)
            {
                IQueryable<Blog> query = db.Blogs
                                           .Include(b => b.BlogImages)
                                           .Where(b => b.DeletedByUserId == null)
                                           .AsQueryable();

                request.Title = request?.Title?.Trim();
                if (!string.IsNullOrWhiteSpace(request.Title))
                    query = query.Where(q => q.Title.StartsWith(request.Title));

                request.Description = request.Description?.Trim();
                if (!string.IsNullOrWhiteSpace(request.Description))
                    query = query.Where(q => q.Description.StartsWith(request.Description));

                query = query.OrderBy(q => q.Id);

                PagedViewModel<Blog> vm = new(query, request.PageIndex, request.PageSize);

                return vm;
            }
        }
    }
}
