using AutoMapper;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.Modules.GenresModule
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
