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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Genre>()
                   .Property(g => g.CreatedDate)
                   .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");
        }
    }
}
