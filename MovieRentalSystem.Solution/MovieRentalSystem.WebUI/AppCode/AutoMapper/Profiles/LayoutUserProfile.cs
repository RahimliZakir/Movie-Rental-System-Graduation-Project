using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.AutoMapper.Dtos;
using MovieRentalSystem.WebUI.AppCode.AutoMapper.Resolvers;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class LayoutUserProfile : Profile
    {
        public LayoutUserProfile()
        {
            CreateMap<AppUserRole, LayoutUserDto>()
                     .ForMember(src => src.ShowName, dest => dest.MapFrom(new LayoutUserResolver()));
        }
    }
}
