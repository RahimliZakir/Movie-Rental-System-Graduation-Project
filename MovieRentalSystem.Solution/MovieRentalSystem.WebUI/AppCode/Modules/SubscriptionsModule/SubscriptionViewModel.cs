using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRentalSystem.WebUI.AppCode.Modules.SubscriptionsModule
{
    public class SubscriptionViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Xahiş olunur mail-inizi daxil edin!")]
        public string? Email { get; set; }

        public bool IsConfirmed { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ConfirmationDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}
