using MovieScoutShared;

namespace MovieScout.Services
{
    public interface IMovieDataService
    {
        Task<IEnumerable<Movie>> GetTrendingMovies();
        Task<Movie>GetMovieDetails(int id);
        Task<IEnumerable<Movie>> GetContent(string s);
        Task<IEnumerable<Movie>> GetSearchResults(string s);
    }
}
