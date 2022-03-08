using AutoMapper;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.AppInfosModule
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
