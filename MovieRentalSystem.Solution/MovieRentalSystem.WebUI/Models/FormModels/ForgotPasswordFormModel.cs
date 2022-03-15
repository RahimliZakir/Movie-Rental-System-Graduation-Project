using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Models.FormModels
{
    public class ForgotPasswordFormModel
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        [EmailAddress(ErrorMessage = "Email formatı düzgün deyil!")]
        public string Email { get; set; } = null!;
    }
}
