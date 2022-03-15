using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Models.FormModels
{
    public class ResetPasswordFormModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifrələr eyni olmalıdır!")]
        public string PasswordConfirm { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
