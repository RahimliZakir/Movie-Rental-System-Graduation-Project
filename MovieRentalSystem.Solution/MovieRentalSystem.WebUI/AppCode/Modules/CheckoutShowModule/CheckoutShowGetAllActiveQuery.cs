using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.CheckoutShowModule
{
    public class CheckoutShowGetAllActiveQuery : IRequest<List<CheckoutShowViewModel>>
    {
        public class CheckoutShowGetAllActiveQueryHandler : IRequestHandler<CheckoutShowGetAllActiveQuery, List<CheckoutShowViewModel>>
        {
            readonly MovieDbContext db;

            public CheckoutShowGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<List<CheckoutShowViewModel>> Handle(CheckoutShowGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                var dt = DateTime.UtcNow.AddHours(4);

                List<CheckoutShowViewModel> vmList = await (from m in db.Shows.Where(m => m.DeletedByUserId == null)
                                                            join mc in db.ShowCheckouts.Where(mc => mc.DeletedByUserId == null && mc.ExpiredDate > dt) on m.Id equals mc.ShowId
                                                            select new CheckoutShowViewModel
                                                            {
                                                                Id = m.Id,
                                                                Name = m.Name,
                                                                ImagePath = m.ImagePath,
                                                                Period = mc.Period,
                                                                PeopleCount = mc.PeopleCount,
                                                                TotalPrice = mc.TotalPrice,
                                                                ExpiredDate = mc.ExpiredDate
                                                            }).ToListAsync(cancellationToken);

                return vmList;
            }
        }
    }
}
