using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.Modules.ShowsModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class ShowProfile : Profile
    {
        public ShowProfile()
        {
            CreateMap<Show, ShowViewModel>();
            CreateMap<ShowCreateCommand, Show>();
            CreateMap<ShowEditCommand, Show>();
        }
    }
}
