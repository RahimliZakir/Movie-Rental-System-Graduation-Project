using MediatR;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.ViewModels;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ContactMessageTypesModule
{
    public class ContactMessageTypePagedQuery : IRequest<PagedViewModel<ContactMessageType>>
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

        public ContactMessageTypePagedQuery() { }

        public ContactMessageTypePagedQuery(int pageIndex = 1, int pageSize = 10)
        {
            this.PageIndex = pageIndex;
            this.pageSize = pageSize;
        }
        #endregion

        public PagedViewModel<ContactMessageType> Response { get; set; }
        public string Text { get; set; }

        public class ContactMessageTypePagedQueryHandler : IRequestHandler<ContactMessageTypePagedQuery, PagedViewModel<ContactMessageType>>
        {
            readonly MovieDbContext db;

            public ContactMessageTypePagedQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<PagedViewModel<ContactMessageType>> Handle(ContactMessageTypePagedQuery request, CancellationToken cancellationToken)
            {
                IQueryable<ContactMessageType> query = db.ContactMessageType.Where(f => f.DeletedDate == null).AsQueryable();

                request.Text = request.Text?.Trim();
                if (!string.IsNullOrWhiteSpace(request.Text))
                    query = query.Where(q => q.Text.Contains(request.Text));

                query = query.OrderBy(q => q.Id);

                PagedViewModel<ContactMessageType> viewModel = new(query, request.PageIndex, request.PageSize);

                return viewModel;
            }
        }
    }
}
