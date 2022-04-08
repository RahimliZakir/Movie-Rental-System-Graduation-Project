using MovieRentalSystem.WebUI.AppCode.Infrastructure;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class ShowCheckout : BaseEntity
    {
        public int ShowId { get; set; }

        public virtual Show Show { get; set; }

        public int Period { get; set; }

        public int PeopleCount { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime? ExpiredDate { get; set; }
    }
}
