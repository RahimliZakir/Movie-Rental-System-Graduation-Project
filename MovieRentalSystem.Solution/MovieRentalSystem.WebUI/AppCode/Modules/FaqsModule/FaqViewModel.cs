using MovieRentalSystem.WebUI.Models.Entities.Membership;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.AppCode.Modules.FaqsModule
{
    public class FaqViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Question { get; set; } = null!;

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Answer { get; set; } = null!;

        public int? CreatedByUserId { get; set; }

        public AppUser? CreatedByUser { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
