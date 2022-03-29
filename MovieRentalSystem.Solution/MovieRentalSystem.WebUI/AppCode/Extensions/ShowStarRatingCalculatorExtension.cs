using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static decimal GetAverageRating(this Show show)
        {
            int fiveStarUsers = show.ShowComments.Count(s => s.StarRating == 5);
            int fourStarUsers = show.ShowComments.Count(s => s.StarRating == 4);
            int threeStarUsers = show.ShowComments.Count(s => s.StarRating == 3);
            int twoStarUsers = show.ShowComments.Count(s => s.StarRating == 2);
            int oneStarUsers = show.ShowComments.Count(s => s.StarRating == 1);

            int allUsers = fiveStarUsers + fourStarUsers + threeStarUsers + twoStarUsers + oneStarUsers;
            if (allUsers == 0)
                return 0;

            decimal average = (fiveStarUsers * 5 + fourStarUsers * 4 + threeStarUsers * 3 + twoStarUsers * 2 + oneStarUsers * 1) / (allUsers);
            average = Math.Round(average, 1);

            return average;
        }
    }
}
