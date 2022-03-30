using MovieRentalSystem.WebUI.Models.Entities.Membership;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class MovieComment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Content { get; set; } = null!;

        public int StarRating { get; set; }

        public bool IsSpoiler { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int? ParentId { get; set; }

        public virtual MovieComment? Parent { get; set; }

        public virtual ICollection<MovieComment>? Children { get; set; }

        public int CreatedByUserId { get; set; }

        public virtual AppUser CreatedByUser { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}
