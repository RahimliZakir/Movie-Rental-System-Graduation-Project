using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public IEnumerable<Show>? Shows { get; set; }

        public IEnumerable<Movie>? Movies { get; set; }
    }
}
