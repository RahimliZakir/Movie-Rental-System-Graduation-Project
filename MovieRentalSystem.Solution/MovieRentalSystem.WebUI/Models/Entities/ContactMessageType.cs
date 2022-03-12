using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class ContactMessageType : BaseEntity
    {
        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Text { get; set; } = null!;

        public virtual ICollection<ContactMessage> ContactMessages { get; set; }
    }
}
