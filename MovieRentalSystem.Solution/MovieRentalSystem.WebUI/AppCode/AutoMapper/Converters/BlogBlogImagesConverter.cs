using AutoMapper;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Converters
{
    public class BlogBlogImagesConverter : IValueConverter<ICollection<BlogImage>, string[]>
    {
        public string[] Convert(ICollection<BlogImage> member, ResolutionContext context)
        {
            string[] images = Array.Empty<string>();

            foreach (BlogImage item in member.Where(bi => bi.IsMain == false))
            {
                Array.Resize(ref images, images.Length + 1);
                images[images.Length - 1] = $"/uploads/blogs/{item.ImagePath}";
            }

            return images;
        }
    }
}
