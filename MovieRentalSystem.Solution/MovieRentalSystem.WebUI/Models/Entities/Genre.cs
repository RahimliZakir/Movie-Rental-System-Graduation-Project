using MovieRentalSystem.WebUI.AppCode.Infrastructure;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; } = null!;

        public int? ParentId { get; set; }

        public virtual Genre Parent { get; set; }

        public virtual ICollection<Genre> Children { get; set; }
    }
}
