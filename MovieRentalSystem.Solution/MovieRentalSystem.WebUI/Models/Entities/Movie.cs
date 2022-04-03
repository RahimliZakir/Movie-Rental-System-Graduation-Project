using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class Movie : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string MovieFrame { get; set; } = null!;

        public bool IsPremium { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public decimal Price { get; set; }

        public string? Duration { get; set; }

        public string? Quality { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int DirectorId { get; set; }

        public virtual Director Director { get; set; }

        public virtual ICollection<MovieComment> MovieComments { get; set; }

        public virtual ICollection<MovieGenreCastItem> MovieGenreCastItems { get; set; }

        public virtual ICollection<MovieCheckout> MovieCheckouts { get; set; }
    }
}
