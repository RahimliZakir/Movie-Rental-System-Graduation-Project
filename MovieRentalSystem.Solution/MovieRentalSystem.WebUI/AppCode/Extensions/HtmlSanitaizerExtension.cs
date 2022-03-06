using System.Text.RegularExpressions;

namespace MovieRentalSystem.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static string RemoveHtmlTags(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            Regex pattern = new("<[^>]*>");

            return pattern.Replace(value, "");
        }
    }
}
