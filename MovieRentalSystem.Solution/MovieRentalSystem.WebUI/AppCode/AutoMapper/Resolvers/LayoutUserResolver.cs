using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.AutoMapper.Dtos;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Resolvers
{
    public class LayoutUserResolver : IValueResolver<AppUserRole, LayoutUserDto, string>
    {
        public string Resolve(AppUserRole src, LayoutUserDto dest, string destMember, ResolutionContext context)
        {
            dest.ImagePath = !string.IsNullOrWhiteSpace(src.User.ImagePath) ? $"/uploads/personalside/{src.User.ImagePath}" : "/uploads/personalside/no-profile-picture.jpg";

            dest.Email = src.User.Email;

            dest.RoleName = src.Role.Name;

            string showName = src.User.UserName;

            if (!string.IsNullOrEmpty(src.User.Name) || !string.IsNullOrWhiteSpace(src.User.Surname))
            {
                showName = $"{src.User.Name} {src.User.Surname}";
            }

            return showName;
        }
    }
}
