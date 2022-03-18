using Microsoft.AspNetCore.Identity;

namespace MovieRentalSystem.WebUI.Models.Entities.Membership
{
    public class AppRole : IdentityRole<int>
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
