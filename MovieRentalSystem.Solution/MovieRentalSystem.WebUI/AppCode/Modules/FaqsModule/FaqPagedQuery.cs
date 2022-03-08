using MediatR;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.ViewModels;

namespace MovieRentalSystem.WebUI.AppCode.Modules.FaqsModule
{
    public class FaqPagedQuery : IRequest<PagedViewModel<Faq>>
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


        public FaqPagedQuery() { }

        public FaqPagedQuery(int pageIndex = 1, int pageSize = 10)
        {
            this.PageIndex = pageIndex;
            this.pageSize = pageSize;
        }
        #endregion

        public PagedViewModel<Faq> Response { get; set; }

        public class FaqPagedQueryHandler : IRequestHandler<FaqPagedQuery, PagedViewModel<Faq>>
        {
            readonly MovieDbContext db;

            public FaqPagedQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<PagedViewModel<Faq>> Handle(FaqPagedQuery request, CancellationToken cancellationToken)
            {
                IQueryable<Faq> query = db.Faqs.Where(f => f.DeletedDate == null).AsQueryable();

                query = query.OrderBy(q => q.Id);

                PagedViewModel<Faq> viewModel = new(query, request.PageIndex, request.PageSize);

                return viewModel;
            }
        }
    }
}
