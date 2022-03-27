using MediatR;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.ViewModels;

namespace MovieRentalSystem.WebUI.AppCode.Modules.CastsModule
{
    public class CastPagedQuery : IRequest<PagedViewModel<Cast>>
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

        public CastPagedQuery() { }

        public CastPagedQuery(int pageIndex = 1, int pageSize = 10)
        {
            this.PageIndex = pageIndex;
            this.pageSize = pageSize;
        }
        #endregion

        public PagedViewModel<Cast> Response { get; set; }
        public string Name { get; set; }

        public class CastPagedQueryHandler : IRequestHandler<CastPagedQuery, PagedViewModel<Cast>>
        {
            readonly MovieDbContext db;

            public CastPagedQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<PagedViewModel<Cast>> Handle(CastPagedQuery request, CancellationToken cancellationToken)
            {
                IQueryable<Cast> query = db.Casts.Where(f => f.DeletedDate == null).AsQueryable();

                request.Name = request.Name?.Trim();
                if (!string.IsNullOrWhiteSpace(request.Name))
                    query = query.Where(q => q.Name.Contains(request.Name));

                query = query.OrderBy(q => q.Id);

                PagedViewModel<Cast> viewModel = new(query, request.PageIndex, request.PageSize);

                return viewModel;
            }
        }
    }
}
