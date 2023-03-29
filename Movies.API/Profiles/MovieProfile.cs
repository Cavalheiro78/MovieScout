using AutoMapper;
using MovieScoutShared;

namespace Movies.API.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile() 
        {
            CreateMap<MovieScout.Entities.MovieEntity, Movie>();
            CreateMap<Movie, MovieScout.Entities.MovieEntity>();
        }
    }
}
