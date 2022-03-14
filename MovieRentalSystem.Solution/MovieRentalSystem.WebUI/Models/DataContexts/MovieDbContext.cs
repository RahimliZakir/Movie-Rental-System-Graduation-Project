﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.Models.DataContexts
{
    public class MovieDbContext :
        IdentityDbContext<AppUser, AppRole, int, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        public MovieDbContext(DbContextOptions options)
            : base(options)
        {

        }

        //---Identity---
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<AppUserClaim> UserClaims { get; set; }
        public DbSet<AppUserRole> UserRoles { get; set; }
        public DbSet<AppUserLogin> UserLogins { get; set; }
        public DbSet<AppRoleClaim> RoleClaims { get; set; }
        public DbSet<AppUserToken> UserTokens { get; set; }
        //---Identity---

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<AppInfo> AppInfos { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<ContactMessageType> ContactMessageTypes { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //---Identity---
            builder.Entity<AppUser>(e =>
            {
                e.ToTable("Users", "Membership");
            });

            builder.Entity<AppRole>(e =>
            {
                e.ToTable("Roles", "Membership");
            });

            builder.Entity<AppUserRole>(e =>
            {
                e.ToTable("UserRoles", "Membership");
            });

            builder.Entity<AppUserClaim>(e =>
            {
                e.ToTable("UserClaims", "Membership");
            });

            builder.Entity<AppRoleClaim>(e =>
            {
                e.ToTable("RoleClaims", "Membership");
            });

            builder.Entity<AppUserToken>(e =>
            {
                e.ToTable("UserTokens", "Membership");
            });

            builder.Entity<AppUserLogin>(e =>
            {
                e.ToTable("UserLogins", "Membership");
            });
            //---Identity---

            builder.Entity<Genre>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");

            builder.Entity<Faq>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");

            builder.Entity<AppInfo>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");

            builder.Entity<Subscription>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");

            builder.Entity<ContactMessageType>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");

            builder.Entity<ContactMessage>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");
        }

        public DbSet<MovieRentalSystem.WebUI.Models.Entities.ContactMessageType> ContactMessageType { get; set; }
    }
}
