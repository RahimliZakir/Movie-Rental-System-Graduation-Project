using AutoMapper;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Converters
{
    public class BlogAuthorConverter : IValueConverter<AppUser, string>
    {
        public string Convert(AppUser member, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(member.Name) && !string.IsNullOrWhiteSpace(member.Surname))
            {
                return $"{member.Name} {member.Surname}";
            }
            else if (!string.IsNullOrWhiteSpace(member.Name))
            {
                return member.Name;
            }
            else if (!string.IsNullOrWhiteSpace(member.Surname))
            {
                return member.Surname;
            }
            else
            {
                return member.UserName;
            }
        }
    }
}
