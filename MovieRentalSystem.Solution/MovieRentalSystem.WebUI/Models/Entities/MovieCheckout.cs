using MovieRentalSystem.WebUI.AppCode.Infrastructure;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class MovieCheckout : BaseEntity
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int Period { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime? ExpiredDate { get; set; }
    }
}
