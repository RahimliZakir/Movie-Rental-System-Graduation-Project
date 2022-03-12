using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.Modules.FaqsModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class FaqProfile : Profile
    {
        public FaqProfile()
        {
            CreateMap<Faq, FaqViewModel>();
            CreateMap<FaqCreateCommand, Faq>();
            CreateMap<FaqEditCommand, Faq>();
        }
    }
}
