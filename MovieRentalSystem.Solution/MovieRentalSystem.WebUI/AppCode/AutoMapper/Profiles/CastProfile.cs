using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.Modules.CastsModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class CastProfile : Profile
    {
        public CastProfile()
        {
            CreateMap<Cast, CastViewModel>();
            CreateMap<CastCreateCommand, Cast>();
            CreateMap<CastEditCommand, Cast>();
        }
    }
}
