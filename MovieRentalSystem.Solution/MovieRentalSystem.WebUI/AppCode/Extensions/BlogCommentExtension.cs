using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static IEnumerable<BlogComment> FillCommentChildren(this BlogComment blogComment)
        {
            if (blogComment.ParentId != null)
                yield return blogComment;

            if (blogComment.Children != null)
            {
                foreach (BlogComment child in blogComment.Children.SelectMany(bc => bc.FillCommentChildren()))
                {
                    yield return child;
                }
            }
        }
    }
}
