using System.ComponentModel.DataAnnotations;

namespace MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels
{
    public class AnswerContactMessageFormModel
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Lastname { get; set; }

        public string? EmailAddress { get; set; }

        public string? ContactMessageType { get; set; }

        public string? Content { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public string Answer { get; set; } = null!;
    }
}
