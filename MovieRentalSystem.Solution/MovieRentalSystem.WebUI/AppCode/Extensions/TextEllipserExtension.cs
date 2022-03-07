namespace MovieRentalSystem.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static string EllipseText(this string value, int maxLength = 75)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            if (value.Length <= maxLength)
                return value;

            return $"{value.Substring(0, maxLength)}...";
        }
    }
}
