using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRentalSystem.WebUI.Models.Entities.Membership
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public virtual AppUser User { get; set; }

        public virtual AppRole Role { get; set; }
    }
}
