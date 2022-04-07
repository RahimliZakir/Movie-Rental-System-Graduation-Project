using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.ViewModels;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ShowsModule
{
    public class ShowPagedQuery : IRequest<PagedViewModel<Show>>
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

        public ShowPagedQuery() { }

        public ShowPagedQuery(int pageIndex = 1, int pageSize = 10)
        {
            this.PageIndex = pageIndex;
            this.pageSize = pageSize;
        }
        #endregion

        public PagedViewModel<Show> Response { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPremium { get; set; }
        public int? DirectorId { get; set; }

        public class ShowPagedQueryHandler : IRequestHandler<ShowPagedQuery, PagedViewModel<Show>>
        {
            readonly MovieDbContext db;

            public ShowPagedQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<PagedViewModel<Show>> Handle(ShowPagedQuery request, CancellationToken cancellationToken)
            {
                IQueryable<Show> query = db.Shows
                                           .Include(s => s.Director)
                                           .Include(s => s.Room)
                                           .ThenInclude(s => s.Seats)
                                           .Include(s => s.ShowComments)
                                           .ThenInclude(s => s.Children)
                                           .Include(s => s.ShowComments)
                                           .ThenInclude(s => s.CreatedByUser)
                                           .Include(s => s.ShowGenreCastItems)
                                           .ThenInclude(s => s.Genre)
                                           .Include(s => s.ShowGenreCastItems)
                                           .ThenInclude(s => s.Cast)
                                           .Where(f => f.DeletedDate == null).AsQueryable();

                request.Name = request.Name?.Trim();
                if (!string.IsNullOrWhiteSpace(request.Name))
                    query = query.Where(q => q.Name.Contains(request.Name));

                request.Description = request.Description?.Trim();
                if (!string.IsNullOrWhiteSpace(request.Description))
                    query = query.Where(q => q.Description.Contains(request.Description));

                if (request.IsPremium)
                    query = query.Where(q => q.IsPremium);

                if (request.DirectorId > 0 && request.DirectorId != null)
                    query = query.Where(q => q.DirectorId == request.DirectorId);

                query = query.OrderBy(q => q.Id);

                PagedViewModel<Show> viewModel = new(query, request.PageIndex, request.PageSize);

                return viewModel;
            }
        }
    }
}
