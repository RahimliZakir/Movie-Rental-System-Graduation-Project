using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Models.ViewModels
{
    public class FilmViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
    }
}
