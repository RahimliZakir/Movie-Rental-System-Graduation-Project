using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.Modules.ContactMessagesModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class ContactMessageProfile : Profile
    {
        public ContactMessageProfile()
        {
            CreateMap<ContactMessage, ContactMessageViewModel>();
            CreateMap<ContactMessageCreateCommand, ContactMessage>();
        }
    }
}
