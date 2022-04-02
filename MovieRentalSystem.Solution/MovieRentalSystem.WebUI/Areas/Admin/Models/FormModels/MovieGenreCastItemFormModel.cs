using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels
{
    public class MovieGenreCastItemFormModel
    {
        public Movie Movie { get; set; }

        public List<MovieGenreCastItem> MovieGenreCastItems { get; set; }
    }
}
