using MovieScout.Entities;
using MovieScoutShared;

namespace Movies.API.Services
{
    public interface IMovieInfoRepository
    {
        Task<IEnumerable<MovieEntity>> GetMoviesAsync();
        Task<MovieEntity> GetMovieAsync(int id, int userid);
        void AddMovie(MovieEntity movie);
        void DeleteMovie(int id, int userid);
        bool MovieExists(int id, int userid);
        Task<bool> SaveChangesAsync();
    }
}
