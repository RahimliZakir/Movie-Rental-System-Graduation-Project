using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.AutoMapper.Dtos;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Resolvers
{
    public class LayoutUserResolver : IValueResolver<AppUser, LayoutUserDto, string>
    {
        public string Resolve(AppUser src, LayoutUserDto dest, string destMember, ResolutionContext context)
        {
            dest.ImagePath = !string.IsNullOrWhiteSpace(src.ImagePath) ? $"/uploads/personalside/{src.ImagePath}" : "/uploads/personalside/no-profile-picture.jpg";

            dest.Email = src.Email;

            dest.RoleName = src?.UserRoles?.FirstOrDefault()?.Role.Name;

            string showName = src.UserName;

            if (!string.IsNullOrEmpty(src.Name) || !string.IsNullOrWhiteSpace(src.Surname))
            {
                showName = $"{src.Name} {src.Surname}";
            }

            return showName;
        }
    }
}
