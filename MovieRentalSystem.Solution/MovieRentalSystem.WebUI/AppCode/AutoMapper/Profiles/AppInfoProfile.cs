using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.Modules.AppInfosModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class AppInfoProfile : Profile
    {
        public AppInfoProfile()
        {
            CreateMap<AppInfo, AppInfoViewModel>();
            CreateMap<AppInfoCreateCommand, AppInfo>();
            CreateMap<AppInfoEditCommand, AppInfo>();
        }
    }
}
