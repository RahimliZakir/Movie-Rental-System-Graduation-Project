using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.DataContexts;
using MovieRentalSystem.WebUI.Models.Entities.Membership;
using MovieRentalSystem.WebUI.Models.Entities.Membership.Credentials;

namespace MovieRentalSystem.WebUI.AppCode.Initializers
{
    public static class DatabaseInitializer
    {
        async public static Task<IApplicationBuilder> InjectData(this IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                MovieDbContext db = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
                RoleManager<AppRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                db.Database.Migrate();

                AppRole roleResult = await roleManager.FindByNameAsync("Admin");

                //---Identity---
                if (roleResult == null)
                {
                    roleResult = new AppRole
                    {
                        Name = "Admin"
                    };

                    IdentityResult roleResponse = await roleManager.CreateAsync(roleResult);

                    if (roleResponse.Succeeded)
                    {
                        AppUser userResult = await userManager.FindByNameAsync("rahimlizakir");

                        if (userResult == null)
                        {
                            userResult = new AppUser
                            {
                                UserName = "rahimlizakir",
                                Email = "zakirer@code.edu.az"
                            };

                            IdentityResult userResponse = await userManager.CreateAsync(userResult, AdminCredential.Pick());

                            if (userResponse.Succeeded)
                            {
                                IdentityResult roleUserResult = await userManager.AddToRoleAsync(userResult, roleResult.Name);
                            }
                        }
                        else
                        {
                            IdentityResult roleUserResult = await userManager.AddToRoleAsync(userResult, roleResult.Name);
                        }
                    }
                }
                else
                {
                    AppUser userResult = await userManager.FindByNameAsync("rahimlizakir");

                    if (userResult == null)
                    {
                        userResult = new AppUser
                        {
                            UserName = "rahimlizakir",
                            Email = "zakirer@code.edu.az"
                        };

                        IdentityResult userResponse = await userManager.CreateAsync(userResult, AdminCredential.Pick());

                        if (userResponse.Succeeded)
                        {
                            IdentityResult roleUserResult = await userManager.AddToRoleAsync(userResult, roleResult.Name);
                        }
                    }
                    else
                    {
                        IdentityResult roleUserResult = await userManager.AddToRoleAsync(userResult, roleResult.Name);
                    }
                }
                //---Identity---
            }

            return app;
        }
    }
}
