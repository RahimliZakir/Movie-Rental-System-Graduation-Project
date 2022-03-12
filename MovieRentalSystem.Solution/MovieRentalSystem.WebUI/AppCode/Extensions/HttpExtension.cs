using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MovieRentalSystem.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static string GetScheme(this IActionContextAccessor ctx)
        {
            string scheme = ctx.ActionContext.HttpContext.Request.Scheme;

            return scheme;
        }

        public static HostString GetHost(this IActionContextAccessor ctx)
        {
            HostString host = ctx.ActionContext.HttpContext.Request.Host;

            return host;
        }
    }
}
