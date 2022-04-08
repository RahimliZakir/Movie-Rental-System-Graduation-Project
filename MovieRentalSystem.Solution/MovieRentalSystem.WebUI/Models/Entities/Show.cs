using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class Show : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Description { get; set; } = null!;

        public string ImagePath { get; set; }

        public bool IsPremium { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public decimal Price { get; set; }

        public string? Duration { get; set; }

        public string? Quality { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int DirectorId { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        public virtual ICollection<ShowCheckout> ShowCheckouts { get; set; }

        public virtual ICollection<ShowComment> ShowComments { get; set; }

        public virtual ICollection<ShowGenreCastItem> ShowGenreCastItems { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual Director Director { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
