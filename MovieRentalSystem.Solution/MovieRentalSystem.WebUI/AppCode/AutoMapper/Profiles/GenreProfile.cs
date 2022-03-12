using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.Modules.GenresModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreViewModel>();
            CreateMap<GenreCreateCommand, Genre>();
            CreateMap<GenreEditCommand, Genre>();
        }
    }
}
