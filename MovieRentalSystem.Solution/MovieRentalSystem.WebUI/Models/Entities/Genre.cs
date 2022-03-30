using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class Genre : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;

        public int? ParentId { get; set; }

        public virtual Genre Parent { get; set; }

        public virtual ICollection<Genre> Children { get; set; }

        public virtual ICollection<ShowGenreCastItem> ShowGenreCastItems { get; set; }
    }
}
