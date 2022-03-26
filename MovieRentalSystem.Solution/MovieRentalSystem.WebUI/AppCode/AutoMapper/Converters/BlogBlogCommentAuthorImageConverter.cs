using AutoMapper;
using MovieRentalSystem.WebUI.Models.Entities;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Converters
{
    public class BlogBlogCommentAuthorImageConverter : IValueConverter<ICollection<BlogComment>, ICollection<BlogComment>>
    {
        public ICollection<BlogComment> Convert(ICollection<BlogComment> member, ResolutionContext context)
        {
            foreach (BlogComment item in member)
            {
                item.CreatedByUser.ImagePath = string.IsNullOrWhiteSpace(item.CreatedByUser.ImagePath) ? "/uploads/personalside/no-profile-picture.jpg" : $"/uploads/personalside/{item.CreatedByUser.ImagePath}";
            }

            return member;
        }
    }
}
