using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.ViewModels;

namespace MovieRentalSystem.WebUI.AppCode.Modules.GenresModule
{
    public class GenrePagedQuery : IRequest<PagedViewModel<Genre>>
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

        public GenrePagedQuery() { }

        public GenrePagedQuery(int pageIndex = 1, int pageSize = 10)
        {
            this.PageIndex = pageIndex;
            this.pageSize = pageSize;
        }
        #endregion

        public PagedViewModel<Genre> Response { get; set; }

        public class GenrePagedQueryHandler : IRequestHandler<GenrePagedQuery, PagedViewModel<Genre>>
        {
            readonly MovieDbContext db;

            public GenrePagedQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<PagedViewModel<Genre>> Handle(GenrePagedQuery request, CancellationToken cancellationToken)
            {
                IQueryable<Genre> query = db.Genres
                                            .Include(g => g.Children)
                                            .Where(f => f.DeletedDate == null).AsQueryable();

                query = query.OrderBy(q => q.Id);

                PagedViewModel<Genre> viewModel = new(query, request.PageIndex, request.PageSize);

                return viewModel;
            }
        }
    }
}
