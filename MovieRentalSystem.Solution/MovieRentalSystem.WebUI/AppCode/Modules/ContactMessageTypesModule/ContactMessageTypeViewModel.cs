using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.Entities.Membership;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.AppCode.Modules.ContactMessageTypesModule
{
    public class ContactMessageTypeViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Text { get; set; } = null!;

        public ICollection<ContactMessage>? ContactMessages { get; set; }

        public int? CreatedByUserId { get; set; }

        public AppUser? CreatedByUser { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
