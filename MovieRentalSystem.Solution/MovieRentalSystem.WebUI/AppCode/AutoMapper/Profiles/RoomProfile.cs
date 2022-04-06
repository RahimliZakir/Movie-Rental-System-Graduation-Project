using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.Modules.RoomsModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomViewModel>();
            CreateMap<RoomCreateCommand, Room>();
            CreateMap<RoomEditCommand, Room>();
        }
    }
}
