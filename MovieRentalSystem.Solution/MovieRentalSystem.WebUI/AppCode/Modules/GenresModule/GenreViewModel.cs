using MovieRentalSystem.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.AppCode.Modules.GenresModule
{
    public class GenreViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Name { get; set; } = null!;

        public int? ParentId { get; set; }

        public Genre? Parent { get; set; }

        public ICollection<Genre>? Children { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}
