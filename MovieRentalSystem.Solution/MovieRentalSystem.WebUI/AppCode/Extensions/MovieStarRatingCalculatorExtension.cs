using MovieRentalSystem.WebUI.AppCode.Modules.MoviesModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static decimal GetAverageRating(this Movie movie)
        {
            decimal fiveStarUsers = movie.MovieComments.Count(s => s.StarRating == 5);
            decimal fourStarUsers = movie.MovieComments.Count(s => s.StarRating == 4);
            decimal threeStarUsers = movie.MovieComments.Count(s => s.StarRating == 3);
            decimal twoStarUsers = movie.MovieComments.Count(s => s.StarRating == 2);
            decimal oneStarUsers = movie.MovieComments.Count(s => s.StarRating == 1);

            decimal allUsers = fiveStarUsers + fourStarUsers + threeStarUsers + twoStarUsers + oneStarUsers;
            if (allUsers == 0)
                return 0;

            decimal average = (fiveStarUsers * 5 + fourStarUsers * 4 + threeStarUsers * 3 + twoStarUsers * 2 + oneStarUsers * 1) / (allUsers);
            average = Math.Round(average, 1);

            return average;
        }

        public static decimal GetAverageRating(this MovieViewModel movie)
        {
            decimal fiveStarUsers = movie.MovieComments.Count(s => s.StarRating == 5);
            decimal fourStarUsers = movie.MovieComments.Count(s => s.StarRating == 4);
            decimal threeStarUsers = movie.MovieComments.Count(s => s.StarRating == 3);
            decimal twoStarUsers = movie.MovieComments.Count(s => s.StarRating == 2);
            decimal oneStarUsers = movie.MovieComments.Count(s => s.StarRating == 1);

            decimal allUsers = fiveStarUsers + fourStarUsers + threeStarUsers + twoStarUsers + oneStarUsers;
            if (allUsers == 0)
                return 0;

            decimal average = (fiveStarUsers * 5 + fourStarUsers * 4 + threeStarUsers * 3 + twoStarUsers * 2 + oneStarUsers * 1) / (allUsers);
            average = Math.Round(average, 1);

            return average;
        }
    }
}
