using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.Entities.Membership;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ShowsModule
{
    public class ShowViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Description { get; set; } = null!;

        public string? ImagePath { get; set; }

        public IFormFile? File { get; set; } = null!;

        public string? FileTemp { get; set; }

        public bool IsPremium { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public decimal Price { get; set; }

        public string? Duration { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int RoomId { get; set; }

        public Room? Room { get; set; }

        public string? Quality { get; set; }

        public virtual ICollection<ShowGenreCastItem>? ShowGenreCastItems { get; set; }

        public ICollection<ShowComment>? ShowComments { get; set; }

        public int CommentCount
        {
            get
            {
                if (ShowComments?.Where(bc => bc.ParentId == null).Count() > 0)
                    return ShowComments.Where(bc => bc.ParentId == null).Count();

                return 0;
            }
        }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public int DirectorId { get; set; }

        public Director? Director { get; set; }

        public int? CreatedByUserId { get; set; }

        public AppUser? CreatedByUser { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
