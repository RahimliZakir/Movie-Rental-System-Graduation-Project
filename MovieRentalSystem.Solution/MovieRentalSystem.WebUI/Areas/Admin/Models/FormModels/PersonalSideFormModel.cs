using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels
{
    public class PersonalSideFormModel
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        [EmailAddress(ErrorMessage = "Email formatı düzgün deyil!")]
        public string EmailAddress { get; set; } = null!;

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? ImagePath { get; set; }

        public IFormFile? File { get; set; }

        public string? FileTemp { get; set; }

        public int? Age { get; set; }

        public string? JobName { get; set; }
    }
}
