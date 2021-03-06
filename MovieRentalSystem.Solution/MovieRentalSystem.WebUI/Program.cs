using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Initializers;
using MovieRentalSystem.WebUI.AppCode.Middlewares;
using MovieRentalSystem.WebUI.AppCode.ModelBinders;
using MovieRentalSystem.WebUI.AppCode.Providers;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities.Membership;
using Newtonsoft.Json;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfiguration conf = builder.Configuration;

IServiceCollection services = builder.Services;
services.AddRouting(cfg =>
{
    cfg.LowercaseUrls = true;
});

services.AddControllersWithViews(cfg =>
{
    cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());

    AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                                     .RequireAuthenticatedUser()
                                     .Build();

    cfg.Filters.Add(new AuthorizeFilter(policy));
})
.AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore); ;

services.AddDbContext<MovieDbContext>(cfg =>
{
    cfg.UseSqlServer(conf.GetConnectionString("cString"));
});

services.AddMediatR(typeof(Program));
services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

services.AddAutoMapper(typeof(Program));

services.AddIdentity<AppUser, AppRole>()
        .AddEntityFrameworkStores<MovieDbContext>()
        .AddDefaultTokenProviders();

services.AddScoped<RoleManager<AppRole>>()
        .AddScoped<UserManager<AppUser>>()
        .AddScoped<SignInManager<AppUser>>();

services.AddScoped<IClaimsTransformation, AppClaimProvider>();

services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
    options.User.RequireUniqueEmail = true;
});

services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

    options.LoginPath = "/signin.html";
    options.AccessDeniedPath = "/accessdenied.html";
    options.SlidingExpiration = true;

    options.Cookie.Name = ".MovieRental.System.Cookie.Analysers";

    options.Cookie.SameSite = SameSiteMode.Strict;
});

services.AddAuthorization(cfg =>
{
    string[] policies = services.GetPolicies(typeof(Program));

    foreach (string policy in policies)
    {
        cfg.AddPolicy(policy, options =>
        {
            options.RequireAssertion(assertion =>
            {
                return assertion.User.HasClaim(policy, "1") || assertion.User.IsInRole("Admin");
            });
        });
    }
});

WebApplication app = builder.Build();
IWebHostEnvironment env = builder.Environment;
if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.InjectData().Wait();

app.UseRouting();

app.UseRequestLocalization(cfg =>
{
    cfg.AddSupportedCultures("en", "az", "ru");
    cfg.AddSupportedUICultures("en", "az", "ru");
    cfg.RequestCultureProviders.Clear(); // Clears all the default culture providers from the list
    cfg.RequestCultureProviders.Add(new AppCultureProvider());
});


app.UseAuthentication();

app.UseAuthorization();

app.UseAuditMiddleware();

app.Use(async (context, next) =>
{
    await next();

    switch (context.Response.StatusCode)
    {
        case 400:
        case 403:
        case 404:
        case 500:
        case 503:
            {
                using (StreamReader sr = new($"Areas/Admin/Views/static/errors/page-error-{context.Response.StatusCode}.html"))
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync(sr.ReadToEnd());
                }
                await next();
                break;
            }
        default:
            break;
    }
});

app.MapGet("/comingsoon.html", async (context) =>
 {
     using (StreamReader sr = new("Views/static/comingsoon.html"))
     {
         context.Response.ContentType = "text/html";
         await context.Response.WriteAsync(await sr.ReadToEndAsync());
     }
 });

app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(name: "SignInRoute", pattern: "signin.html", defaults: new
    {
        controller = "Account",
        action = "SignIn"
    });

    endpoints.MapControllerRoute(name: "RegisterRoute", pattern: "register.html", defaults: new
    {
        controller = "Account",
        action = "Register"
    });

    endpoints.MapControllerRoute(name: "AccessDeniedRoute", pattern: "accessdenied.html", defaults: new
    {
        controller = "Account",
        action = "AccessDenied"
    });

    endpoints.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=PersonalSide}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(name: "default", pattern: "{lang=temp}/{controller=Home}/{action=Index}/{id?}",
                    constraints: new
                    {
                        lang = "az|en|ru|temp"
                    });
});

app.Run();
