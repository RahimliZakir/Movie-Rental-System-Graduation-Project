using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.Modules.ContactMessageTypesModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class ContactMessageTypeProfile : Profile
    {
        public ContactMessageTypeProfile()
        {
            CreateMap<ContactMessageType, ContactMessageTypeViewModel>();
            CreateMap<ContactMessageTypeCreateCommand, ContactMessageType>();
            CreateMap<ContactMessageTypeEditCommand, ContactMessageType>();
        }
    }
}
