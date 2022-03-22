using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.Areas.Admin.Models.ViewModels
{
    public class UserPolicyViewModel
    {
        public AppUser User { get; set; }

        public List<Tuple<AppRole, bool>> Roles { get; set; }

        public List<Tuple<string, bool>> Claims { get; set; }
    }
}
