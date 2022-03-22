using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Models.Entities
{
    public class BlogImage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string ImagePath { get; set; } = null!;

        public bool IsMain { get; set; }

        public int BlogId { get; set; }

        public virtual Blog? Blog { get; set; }
    }
}
