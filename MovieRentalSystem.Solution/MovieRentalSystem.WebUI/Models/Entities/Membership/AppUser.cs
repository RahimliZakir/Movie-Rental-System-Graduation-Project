using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRentalSystem.WebUI.Models.Entities.Membership
{
    public class AppUser : IdentityUser<int>
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? ImagePath { get; set; }

        public int? Age { get; set; }

        public string? JobName { get; set; }

        public virtual ICollection<AppUserRole> UserRoles { get; set; }

        [NotMapped]
        public string? FileTemp { get; set; }
    }
}
