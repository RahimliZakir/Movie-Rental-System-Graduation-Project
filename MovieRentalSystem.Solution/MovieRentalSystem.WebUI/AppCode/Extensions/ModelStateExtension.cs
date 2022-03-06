using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MovieRentalSystem.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static bool IsValid(this IActionContextAccessor ctx)
        {
            return ctx.ActionContext.ModelState.IsValid;
        }

        public static IActionContextAccessor AddModelError(this IActionContextAccessor ctx, string key, string message)
        {
            ctx.ActionContext.ModelState.AddModelError(key, message);

            return ctx;
        }
    }
}
