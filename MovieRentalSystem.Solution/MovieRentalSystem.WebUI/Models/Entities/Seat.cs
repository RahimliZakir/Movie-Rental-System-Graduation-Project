using MovieRentalSystem.WebUI.AppCode.Infrastructure;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class Seat : BaseEntity
    {
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
    }
}
