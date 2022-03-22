using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.Modules.BlogModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogViewModel>();
            CreateMap<BlogCreateCommand, Blog>();
            CreateMap<BlogEditCommand, Blog>();
        }
    }
}
