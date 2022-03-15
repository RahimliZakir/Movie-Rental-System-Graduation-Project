using MovieRentalSystem.WebUI.AppCode.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class Subscription : HistoryWatch
    {
        [Required(ErrorMessage = "Xahiş olunur mail-inizi daxil edin!")]
        public string? Email { get; set; }

        public bool IsConfirmed { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ConfirmationDate { get; set; }
    }
}
