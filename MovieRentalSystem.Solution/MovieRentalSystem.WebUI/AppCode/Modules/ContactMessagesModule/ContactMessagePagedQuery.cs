using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.ViewModels;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ContactMessagesModule
{
    public class ContactMessagePagedQuery : IRequest<PagedViewModel<ContactMessage>>
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

        public ContactMessagePagedQuery() { }

        public ContactMessagePagedQuery(int pageIndex = 1, int pageSize = 10)
        {
            this.PageIndex = pageIndex;
            this.pageSize = pageSize;
        }
        #endregion

        public PagedViewModel<ContactMessage> Response { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string Content { get; set; }
        public string Answer { get; set; }

        public class ContactMessagePagedQueryHandler : IRequestHandler<ContactMessagePagedQuery, PagedViewModel<ContactMessage>>
        {
            readonly MovieDbContext db;

            public ContactMessagePagedQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<PagedViewModel<ContactMessage>> Handle(ContactMessagePagedQuery request, CancellationToken cancellationToken)
            {
                IQueryable<ContactMessage> query = db.ContactMessages
                                                     .Include(c => c.ContactMessageType)
                                                     .Where(f => f.DeletedDate == null)
                                                     .AsQueryable();

                request.Name = request.Name?.Trim();
                if (!string.IsNullOrWhiteSpace(request.Name))
                    query = query.Where(q => q.Name.Contains(request.Name));

                request.Lastname = request.Lastname?.Trim();
                if (!string.IsNullOrWhiteSpace(request.Lastname))
                    query = query.Where(q => q.Lastname.Contains(request.Lastname));

                request.EmailAddress = request.EmailAddress?.Trim();
                if (!string.IsNullOrWhiteSpace(request.EmailAddress))
                    query = query.Where(q => q.EmailAddress.Contains(request.EmailAddress));

                request.Content = request.Content?.Trim();
                if (!string.IsNullOrWhiteSpace(request.Content))
                    query = query.Where(q => q.Content.Contains(request.Content));

                request.Answer = request.Answer?.Trim();
                if (!string.IsNullOrWhiteSpace(request.Answer))
                    query = query.Where(q => q.Answer.Contains(request.Answer));

                query = query.OrderBy(q => q.Id);

                PagedViewModel<ContactMessage> viewModel = new(query, request.PageIndex, request.PageSize);

                return viewModel;
            }
        }
    }
}
