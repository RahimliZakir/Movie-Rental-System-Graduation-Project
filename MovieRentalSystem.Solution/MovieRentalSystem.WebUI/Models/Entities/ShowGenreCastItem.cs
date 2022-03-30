using MovieRentalSystem.WebUI.Models.Entities.Membership;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class ShowGenreCastItem
    {
        public int ShowId { get; set; }

        public virtual Show Show { get; set; }

        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public int CastId { get; set; }

        public virtual Cast Cast { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

        public int CreatedByUserId { get; set; }

        public virtual AppUser CreatedByUser { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedByUserId { get; set; }

        public virtual AppUser? DeletedByUser { get; set; }
    }
}
