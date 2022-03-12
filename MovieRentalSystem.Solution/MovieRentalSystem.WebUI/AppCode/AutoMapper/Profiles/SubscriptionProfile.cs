using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.Modules.SubscriptionsModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class SubscriptionProfile : Profile
    {
        public SubscriptionProfile()
        {
            CreateMap<Subscription, SubscriptionViewModel>();
            CreateMap<SubscriptionCreateCommand, Subscription>();
        }
    }
}
