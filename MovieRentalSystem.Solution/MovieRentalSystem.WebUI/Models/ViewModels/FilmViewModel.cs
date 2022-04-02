using MovieRentalSystem.WebUI.AppCode.Modules.MoviesModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Models.ViewModels
{
    public class FilmViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public IEnumerable<Movie> Movies { get; set; }

        public MovieViewModel Movie { get; set; }

        public IEnumerable<Movie> RelatedMovies { get; set; }
    }
}
