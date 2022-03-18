using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels
{
    public class ConfirmEmailFormModel
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        [EmailAddress(ErrorMessage = "Email formatı düzgün deyil!")]
        public string Email { get; set; }
    }
}
