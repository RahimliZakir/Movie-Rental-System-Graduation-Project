using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.AutoMapper.Converters;
using MovieRentalSystem.WebUI.AppCode.AutoMapper.Dtos;
using MovieRentalSystem.WebUI.AppCode.Extensions;
using MovieRentalSystem.WebUI.AppCode.Modules.BlogModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            // CQRS
            CreateMap<Blog, BlogViewModel>()
                    .ReverseMap();
            CreateMap<BlogCreateCommand, Blog>();
            CreateMap<BlogEditCommand, Blog>();
            // CQRS

            // User
            CreateMap<Blog, BlogDto>()
                    .ForMember(dest => dest.MainImage, src => src.MapFrom(map => $"/uploads/blogs/{map.BlogImages.FirstOrDefault(bi => bi.IsMain).ImagePath}"))
                    .ForMember(dest => dest.ShortDesciption, src => src.MapFrom(map => map.Description.EllipseText(100)))
                    .ForMember(dest => dest.Author, src => src.ConvertUsing(new BlogAuthorConverter(), src => src.CreatedByUser))
                    .ForMember(dest => dest.DetailedOtherImages, src => src.ConvertUsing(new BlogBlogImagesConverter(), src => src.BlogImages))
                    .ForMember(dest => dest.BlogComments, src => src.ConvertUsing(new BlogBlogCommentAuthorImageConverter(), src => src.BlogComments));
            // User
        }
    }
}
