using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static IEnumerable<MovieComment> FillCommentChildren(this MovieComment movieComment)
        {
            if (movieComment.ParentId != null)
                yield return movieComment;

            if (movieComment.Children != null)
            {
                foreach (MovieComment child in movieComment.Children.SelectMany(bc => bc.FillCommentChildren()))
                {
                    yield return child;
                }
            }
        }
    }
}
