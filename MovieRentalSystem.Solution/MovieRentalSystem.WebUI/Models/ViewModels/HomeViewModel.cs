using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Show>? Shows { get; set; }

        public IEnumerable<Show>? LatestShows { get; set; }

        public IEnumerable<Movie>? LatestMovies { get; set; }
    }
}
