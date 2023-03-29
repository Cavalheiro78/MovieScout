using MovieScout.Entities;
using MovieScoutShared;

namespace Movies.API.Services
{
    public interface IMovieInfoRepository
    {
        Task<IEnumerable<MovieEntity>> GetMoviesAsync();
        Task<MovieEntity> GetMovieAsync(int id);
        void AddMovie(MovieEntity movie);
        void DeleteMovie(int id);
        bool MovieExists(int id);
        Task<bool> SaveChangesAsync();
    }
}
