using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MovieRentalSystem.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static string[] GetPolicies<T>(this T services, Type type)
        {
            try
            {
                Type[] types = type.Assembly.GetTypes();

                string[] principals = types
                                      .Where(t => typeof(Controller).IsAssignableFrom(t)
                                      && t.IsDefined(typeof(AuthorizeAttribute), true))
                                      .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
                                      .Union(types
                                      .Where(t => typeof(Controller).IsAssignableFrom(t))
                                      .SelectMany(type => type.GetMethods())
                                      .Where(method => method.IsPublic
                                      && !method.IsDefined(typeof(NonActionAttribute))
                                      && method.IsDefined(typeof(AuthorizeAttribute)))
                                      .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>()))
                                      .Where(a => !string.IsNullOrWhiteSpace(a.Policy))
                                      .SelectMany(a => a.Policy.Split(new[] { ',' },
                                      StringSplitOptions.RemoveEmptyEntries))
                                      .Distinct()
                                      .ToArray();

                return principals;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
