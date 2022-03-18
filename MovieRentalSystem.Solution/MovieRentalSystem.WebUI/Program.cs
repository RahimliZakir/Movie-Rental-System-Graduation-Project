using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Initializers;
using MovieRentalSystem.WebUI.AppCode.ModelBinders;
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

services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(25);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
    options.User.RequireUniqueEmail = true;
});

services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(13);

    options.LoginPath = "/signin.html";
    options.AccessDeniedPath = "/accessdenied.html";
    options.SlidingExpiration = true;

    options.Cookie.Name = ".MovieRental.System.Cookie.Analysers";

    options.Cookie.SameSite = SameSiteMode.Strict;
});

services.AddAuthorization(cfg =>
{
    cfg.AddPolicy("", options =>
    {
        options.RequireAssertion(assertion =>
        {
            return assertion.User.HasClaim("", "1") || assertion.User.IsInRole("Admin");
        });
    });
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

app.UseAuthentication();

app.UseAuthorization();

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

    endpoints.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Genres}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
