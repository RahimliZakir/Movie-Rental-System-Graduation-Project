using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels
{
    public class ShowGenreCastItemFormModel
    {
        public Show Show { get; set; }

        public List<ShowGenreCastItem> ShowGenreCastItems { get; set; }
    }
}
