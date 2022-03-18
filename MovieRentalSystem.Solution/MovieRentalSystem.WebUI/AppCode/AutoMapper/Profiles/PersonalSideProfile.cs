using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.Modules.PersonalSideModule;
using MovieRentalSystem.WebUI.Areas.Admin.Models.FormModels;
using MovieRentalSystem.WebUI.Models.Entities.Membership;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class PersonalSideProfile : Profile
    {
        public PersonalSideProfile()
        {
            CreateMap<AppUser, PersonalSideFormModel>()
                     .ForMember(src => src.Username, dest => dest.MapFrom(map => map.UserName))
                     .ForMember(src => src.EmailAddress, dest => dest.MapFrom(map => map.Email));

            CreateMap<PersonalSideEditCommand, AppUser>()
                     .ForMember(src => src.UserName, dest => dest.MapFrom(map => map.Username))
                     .ForMember(src => src.Email, dest => dest.MapFrom(map => map.EmailAddress))
                     .ForMember(src => src.NormalizedUserName, dest => dest.MapFrom(map => map.Username.ToUpper()))
                     .ForMember(src => src.NormalizedEmail, dest => dest.MapFrom(map => map.EmailAddress.ToUpper()));
        }
    }
}
