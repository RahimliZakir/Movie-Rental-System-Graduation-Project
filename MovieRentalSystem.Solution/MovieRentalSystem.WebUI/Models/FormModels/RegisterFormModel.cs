using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Models.FormModels
{
    public class RegisterFormModel
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        [Compare("Password", ErrorMessage = "Şifrələr eyni olmalıdır!")]
        public string PasswordConfirm { get; set; } = null!;
    }
}
