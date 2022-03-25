using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class Blog : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Description { get; set; } = null!;

        public virtual ICollection<BlogImage>? BlogImages { get; set; }

        public virtual ICollection<BlogLike> BlogLikes { get; set; }

        public virtual ICollection<BlogUnlike> BlogUnlikes { get; set; }

        [NotMapped]
        public ImageItem[] Files { get; set; }
    }
}
