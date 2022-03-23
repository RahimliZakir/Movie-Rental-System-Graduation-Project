using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities;
using System.Threading.Tasks;

namespace MovieRentalSystem.WebUI.AppCode.Middlewares
{
    public class AuditMiddleware
    {
        readonly RequestDelegate next;

        public AuditMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            using (IServiceScope scope = httpContext.RequestServices.CreateScope())
            {
                MovieDbContext db = scope.ServiceProvider.GetRequiredService<MovieDbContext>();

                AuditLog audit = new()
                {
                    RequestTime = DateTime.UtcNow.AddHours(4)
                };
                audit.Path = httpContext.Request.Path;
                audit.IsHttps = httpContext.Request.IsHttps;
                audit.Method = httpContext.Request.Method;

                if (!string.IsNullOrWhiteSpace(httpContext.Request.QueryString.Value))
                    audit.QueryString = httpContext.Request.QueryString.Value;

                RouteData routeData = httpContext.GetRouteData();

                if (routeData.Values.TryGetValue("area", out object area) && !string.IsNullOrWhiteSpace(area?.ToString()))
                    audit.Area = area?.ToString();
                if (routeData.Values.TryGetValue("controller", out object controller) && !string.IsNullOrWhiteSpace(controller?.ToString()))
                    audit.Controller = controller?.ToString();
                if (routeData.Values.TryGetValue("action", out object action) && !string.IsNullOrWhiteSpace(action?.ToString()))
                    audit.Action = action?.ToString();

                if (httpContext.User.Identity.IsAuthenticated)
                {
                    int userId = httpContext.User.GetUserId();
                    if (userId > 0 && userId != null)
                        audit.CreatedByUserId = userId;
                }

                try
                {
                    await this.next(httpContext);
                }
                catch (Exception ex)
                {

                    //throw;
                }

                audit.StatusCode = httpContext.Response.StatusCode;
                audit.ResponseTime = DateTime.UtcNow.AddHours(4);

                await db.AuditLogs.AddAsync(audit);
                await db.SaveChangesAsync();
            }
        }
    }

    public static class AuditMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuditMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuditMiddleware>();
        }
    }
}
