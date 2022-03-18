using System.Text.RegularExpressions;

namespace MovieRentalSystem.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static bool ValidateUrl(this string content)
        {
            Regex pattern = new(@"/^((ftp|http|https):\/\/)?(www.)?(?!.*(ftp|http|https|www.))[a-zA-Z0-9_-]+(\.[a-zA-Z]+)+((\/)[\w#]+)*(\/\w+\?[a-zA-Z0-9_]+=\w+(&[a-zA-Z0-9_]+=\w+)*)?$/gm");

            if (pattern.IsMatch(content)) return true;
            else return false;
        }

        public static bool ValidateRoute(this string content)
        {
            Regex pattern = new(@"/[^-:\/_{}()a-zA-Z\d]/g");

            if (pattern.IsMatch(content)) return true;
            else return false;
        }
    }
}