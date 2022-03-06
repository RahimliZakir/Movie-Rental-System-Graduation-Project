using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using Newtonsoft.Json;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfiguration conf = builder.Configuration;

IServiceCollection services = builder.Services;
services.AddRouting(cfg =>
{
    cfg.LowercaseUrls = true;
});

services.AddControllersWithViews()
        .AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore); ;

services.AddDbContext<MovieDbContext>(cfg =>
{
    cfg.UseSqlServer(conf.GetConnectionString("cString"));
});

services.AddMediatR(typeof(Program));
services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

services.AddAutoMapper(typeof(Program));

WebApplication app = builder.Build();
IWebHostEnvironment env = builder.Environment;
if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Genres}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
