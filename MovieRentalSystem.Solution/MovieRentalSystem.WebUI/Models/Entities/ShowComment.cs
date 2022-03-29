using MovieRentalSystem.WebUI.Models.Entities.Membership;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class ShowComment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Content { get; set; } = null!;

        public int ShowId { get; set; }

        public virtual Show Show { get; set; }

        public int? ParentId { get; set; }

        public virtual ShowComment? Parent { get; set; }

        public virtual ICollection<ShowComment>? Children { get; set; }

        public int CreatedByUserId { get; set; }

        public virtual AppUser CreatedByUser { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}
