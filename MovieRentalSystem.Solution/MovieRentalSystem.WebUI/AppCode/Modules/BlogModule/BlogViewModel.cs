using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.Entities.Membership;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.AppCode.Modules.BlogModule
{
    public class BlogViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Description { get; set; } = null!;

        public int? CreatedByUserId { get; set; }

        public AppUser? CreatedByUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<BlogImage>? BlogImages { get; set; }

        public ICollection<BlogComment>? BlogComments { get; set; }

        public int CommentCount
        {
            get
            {
                if (BlogComments?.Where(bc => bc.ParentId == null).Count() > 0)
                    return BlogComments.Where(bc => bc.ParentId == null).Count();

                return 0;
            }
        }

        public ImageItem[] Files { get; set; }
    }
}
