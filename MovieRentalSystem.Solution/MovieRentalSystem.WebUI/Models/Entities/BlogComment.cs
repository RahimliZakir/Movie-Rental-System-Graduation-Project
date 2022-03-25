using MovieRentalSystem.WebUI.Models.Entities.Membership;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class BlogComment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Content { get; set; } = null!;

        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }

        public int? ParentId { get; set; }

        public virtual BlogComment? Parent { get; set; }

        public virtual ICollection<BlogComment>? Children { get; set; }

        public int CreatedByUserId { get; set; }

        public virtual AppUser CreatedByUser { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}
