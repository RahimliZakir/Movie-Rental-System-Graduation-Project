using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.CheckoutMovieModule
{
    public class CheckoutMovieGetAllActiveQuery : IRequest<List<CheckoutMovieViewModel>>
    {
        public class CheckoutMovieGetAllActiveQueryHandler : IRequestHandler<CheckoutMovieGetAllActiveQuery, List<CheckoutMovieViewModel>>
        {
            readonly MovieDbContext db;

            public CheckoutMovieGetAllActiveQueryHandler(MovieDbContext db)
            {
                this.db = db;
            }

            async public Task<List<CheckoutMovieViewModel>> Handle(CheckoutMovieGetAllActiveQuery request, CancellationToken cancellationToken)
            {
                //List<CheckoutMovieViewModel> vmList = new();

                DateTime dt = DateTime.UtcNow.AddHours(4);

                //IEnumerable<MovieCheckout> checkouts = await db.MovieCheckouts
                //                                               .Where(mc => mc.DeletedByUserId == null && mc.ExpiredDate > dt)
                //                                               .ToListAsync(cancellationToken);

                //int counter = 0;
                //foreach (var item in checkouts)
                //{
                //    Movie movie = await db.Movies.FirstOrDefaultAsync(m => m.Id == item.MovieId, cancellationToken);

                //    vmList[counter].Id = movie.Id;
                //    vmList[counter].Name = movie.Name;
                //    vmList[counter].MovieFrame = movie.MovieFrame;
                //    vmList[counter].Period = item.Period;
                //    vmList[counter].TotalPrice = item.TotalPrice;
                //    vmList[counter].ExpiredDate = item.ExpiredDate;

                //    counter++;
                //}

                List<CheckoutMovieViewModel> vmList = await (from m in db.Movies.Where(m => m.DeletedByUserId == null)
                                                             join mc in db.MovieCheckouts.Where(mc => mc.DeletedByUserId == null && mc.ExpiredDate > dt) on m.Id equals mc.MovieId
                                                             select new CheckoutMovieViewModel
                                                             {
                                                                 Id = m.Id,
                                                                 Name = m.Name,
                                                                 MovieFrame = m.MovieFrame,
                                                                 Period = mc.Period,
                                                                 TotalPrice = mc.TotalPrice,
                                                                 ExpiredDate = mc.ExpiredDate
                                                             }).ToListAsync(cancellationToken);

                return vmList;
            }
        }
    }
}
