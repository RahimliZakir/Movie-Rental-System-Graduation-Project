using MovieRentalSystem.WebUI.Models.Entities.Membership;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRentalSystem.WebUI.AppCode.Infrastructure
{
    public class HistoryWatch
    {
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

        public int? DeletedByUserId { get; set; }

        public virtual AppUser DeletedByUser { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
