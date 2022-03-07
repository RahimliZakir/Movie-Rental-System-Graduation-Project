using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Models.DataContexts
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Faq> Faqs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Genre>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");

            builder.Entity<Faq>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");
        }
    }
}
