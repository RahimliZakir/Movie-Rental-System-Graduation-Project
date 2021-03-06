using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class Cast : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;

        public virtual ICollection<ShowGenreCastItem> ShowGenreCastItems { get; set; }

        public virtual ICollection<MovieGenreCastItem> MovieGenreCastItems { get; set; }
    }
}
