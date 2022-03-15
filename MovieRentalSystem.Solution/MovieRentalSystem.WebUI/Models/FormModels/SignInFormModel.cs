using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Models.FormModels
{
    public class SignInFormModel
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Password { get; set; } = null!;
    }
}
