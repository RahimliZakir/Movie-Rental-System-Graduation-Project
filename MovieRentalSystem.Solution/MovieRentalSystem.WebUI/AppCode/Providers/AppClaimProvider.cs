﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.Models.DataContexts;
using System.Security.Claims;

namespace MovieRentalSystem.WebUI.AppCode.Providers
{
    public class AppClaimProvider : IClaimsTransformation
    {
        readonly MovieDbContext db;
        public AppClaimProvider(MovieDbContext db)
        {
            this.db = db;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity is ClaimsIdentity claimIdentity
                && claimIdentity.IsAuthenticated)
            {

                int? currentUserId = principal.GetUserId();

                var roleClaim = claimIdentity.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role));
                while (roleClaim != null)
                {
                    claimIdentity.RemoveClaim(roleClaim);
                    roleClaim = claimIdentity.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role));
                }

                string[] policiesExtension = principal.GetPolicies(typeof(AppClaimProvider));

                foreach (var policy in policiesExtension)
                {
                    var policyClaim = claimIdentity.Claims.FirstOrDefault(c => c.Type.Equals(policy));

                    if (policyClaim != null)
                        claimIdentity.RemoveClaim(policyClaim);
                }

                var roles = await (from ur in db.UserRoles
                                   join r in db.Roles on ur.RoleId equals r.Id
                                   where ur.UserId == currentUserId
                                   select r.Name).ToArrayAsync();

                foreach (var role in roles)
                {
                    claimIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                }


                var policies = await (from rc in db.RoleClaims
                                      join ur in db.UserRoles on rc.RoleId equals ur.RoleId
                                      where ur.UserId == currentUserId && rc.ClaimValue.Equals("1")
                                      select rc.ClaimType
                                )
                                .Union(from uc in db.UserClaims
                                       where uc.UserId == currentUserId && uc.ClaimValue.Equals("1")
                                       select uc.ClaimType)
                                .Distinct()
                                .ToArrayAsync();

                foreach (var policy in policies)
                {
                    claimIdentity.AddClaim(new Claim(policy, "1"));
                }


                var currentUser = await db.Users.FirstOrDefaultAsync(u => u.Id == currentUserId);

                if (!string.IsNullOrWhiteSpace(currentUser.Name) || !string.IsNullOrWhiteSpace(currentUser.Surname))
                {
                    claimIdentity.AddClaim(new Claim("FullName", $"{currentUser.Name} {currentUser.Surname}"));
                }
                else if (!string.IsNullOrWhiteSpace(currentUser.PhoneNumber))
                {
                    claimIdentity.AddClaim(new Claim("FullName", $"{currentUser.PhoneNumber}"));
                }
                else
                {
                    claimIdentity.AddClaim(new Claim("FullName", $"{currentUser.Email}"));
                }
            }


            return principal;
        }
    }
}
