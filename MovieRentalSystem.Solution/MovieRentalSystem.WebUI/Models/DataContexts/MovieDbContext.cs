using Microsoft.EntityFrameworkCore;

namespace MovieRentalSystem.WebUI.Models.DataContexts
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions options)
            : base(options)
        {

        }


    }
}
