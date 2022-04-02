using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.Entities.Membership;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.AppCode.Modules.MoviesModule
{
    public class MovieViewModel
    {
        public int? Id { get; set; }

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

        public Director? Director { get; set; }

        public virtual ICollection<MovieGenreCastItem>? MovieGenreCastItems { get; set; }

        public ICollection<MovieComment>? MovieComments { get; set; }

        public int CommentCount
        {
            get
            {
                if (MovieComments?.Where(bc => bc.ParentId == null).Count() > 0)
                    return MovieComments.Where(bc => bc.ParentId == null).Count();

                return 0;
            }
        }

        public int? CreatedByUserId { get; set; }

        public AppUser? CreatedByUser { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
