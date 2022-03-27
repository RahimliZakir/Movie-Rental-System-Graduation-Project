using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.Modules.DirectorsModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class DirectorProfile : Profile
    {
        public DirectorProfile()
        {
            CreateMap<Director, DirectorViewModel>();
            CreateMap<DirectorCreateCommand, Director>();
            CreateMap<DirectorEditCommand, Director>();
        }
    }
}
