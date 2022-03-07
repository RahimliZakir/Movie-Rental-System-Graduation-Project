using AutoMapper;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.FaqsModule
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
