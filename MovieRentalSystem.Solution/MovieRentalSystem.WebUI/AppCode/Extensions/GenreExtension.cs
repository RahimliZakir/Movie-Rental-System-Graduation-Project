using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static IEnumerable<Genre> FillGenres(this Genre parent)
        {
            if (parent.ParentId != null)
                yield return parent;

            if (parent.Children != null)
            {
                foreach (Genre child in parent.Children.SelectMany(c => c.FillGenres()))
                {
                    yield return child;
                }
            }
        }
    }
}
