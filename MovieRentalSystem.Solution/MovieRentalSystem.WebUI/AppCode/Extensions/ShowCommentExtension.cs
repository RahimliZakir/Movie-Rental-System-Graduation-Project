using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static IEnumerable<ShowComment> FillCommentChildren(this ShowComment showComment)
        {
            if (showComment.ParentId != null)
                yield return showComment;

            if (showComment.Children != null)
            {
                foreach (ShowComment child in showComment.Children.SelectMany(bc => bc.FillCommentChildren()))
                {
                    yield return child;
                }
            }
        }
    }
}
