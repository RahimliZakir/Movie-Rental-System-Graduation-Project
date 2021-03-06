using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        //---Audit---
        public DbSet<AuditLog> AuditLogs { get; set; }
        //---Audit---

        //---Cinema---
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Room> Rooms { get; set; }
        //---Cinema---

        //---Shows---
        public DbSet<Director> Directors { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<ShowComment> ShowComments { get; set; }
        public DbSet<ShowGenreCastItem> ShowGenreCastItems { get; set; }
        //---Shows---

        //---Movie---
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieComment> MovieComments { get; set; }
        public DbSet<MovieGenreCastItem> MovieGenreCastItems { get; set; }
        //---Movie---

        //---Blogs---
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogLike> BlogLikes { get; set; }
        public DbSet<BlogUnlike> BlogUnlikes { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        //---Blogs---

        //---Checkout---
        public DbSet<MovieCheckout> MovieCheckouts { get; set; }
        public DbSet<ShowCheckout> ShowCheckouts { get; set; }
        //---Checkout---

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //---Identity---
            builder.Entity<AppUser>(e =>
            {
                e.HasMany(c => c.UserRoles)
                 .WithOne(c => c.User)
                 .HasForeignKey(c => c.UserId)
                 .IsRequired();

                e.ToTable("Users", "Membership");
            });

            builder.Entity<AppRole>(e =>
            {
                e.HasMany(c => c.UserRoles)
                 .WithOne(c => c.Role)
                 .HasForeignKey(c => c.RoleId)
                 .IsRequired();

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

            //---Blogs---
            builder.Entity<Blog>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");
            //---Blogs---

            //---Cinema---
            builder.Entity<Seat>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");

            builder.Entity<Room>()
                  .Property(g => g.CreatedDate)
                  .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");
            //---Cinema---

            //---Shows---
            builder.Entity<Director>()
                       .Property(g => g.CreatedDate)
                       .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");

            builder.Entity<Cast>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");

            builder.Entity<Show>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");

            builder.Entity<MovieGenreCastItem>(cfg =>
            {
                cfg.HasKey(x => new { x.MovieId, x.CastId, x.GenreId });
            });
            builder.Entity<MovieGenreCastItem>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");

            builder.Entity<ShowGenreCastItem>(cfg =>
            {
                cfg.HasKey(x => new { x.ShowId, x.CastId, x.GenreId });
            });
            builder.Entity<ShowGenreCastItem>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");
            //---Shows---

            //---Movie---
            builder.Entity<Movie>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");
            //---Movie---

            //---Checkout---
            builder.Entity<MovieCheckout>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");

            builder.Entity<ShowCheckout>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");
            //---Checkout---
        }
    }
}
