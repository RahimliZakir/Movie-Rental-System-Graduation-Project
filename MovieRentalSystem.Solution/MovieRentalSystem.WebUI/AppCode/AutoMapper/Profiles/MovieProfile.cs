using AutoMapper;
using MovieRentalSystem.WebUI.AppCode.Modules.MoviesModule;
using MovieRentalSystem.WebUI.Models.Entities;

namespace MovieRentalSystem.WebUI.AppCode.AutoMapper.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieViewModel>();
            CreateMap<MovieCreateCommand, Movie>();
            CreateMap<MovieEditCommand, Movie>();
        }
    }
}
