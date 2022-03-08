using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class AppInfo : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Map { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string HeaderContent { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string FooterContent { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string FacebookLink { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string TwitterLink { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string LinkedinLink { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string TelegramLink { get; set; } = null!;
    }
}
