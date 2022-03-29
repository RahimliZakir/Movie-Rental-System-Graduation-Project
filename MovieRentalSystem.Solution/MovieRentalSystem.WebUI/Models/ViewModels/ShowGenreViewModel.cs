using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Models.ViewModels
{
    public class ShowGenreViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public IEnumerable<Show> Shows { get; set; }
    }
}
