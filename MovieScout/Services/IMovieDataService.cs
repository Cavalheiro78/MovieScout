using MovieScoutShared;
using Refit;

namespace MovieScout.Services
{
    public interface IMovieDataService
    {
        [Get("/trending/movie/day?api_key=42732b85a50a57a93e9344f93cee98d1")]
        Task<Page> GetTrendingMovies();

        [Get("/movie/{id}?api_key=42732b85a50a57a93e9344f93cee98d1")]
        Task<Movie>GetMovieDetails(int id);

        [Get("/trending/movie/{**s}?api_key=42732b85a50a57a93e9344f93cee98d1")]
        Task<Page> GetContent(string s);

        [Get("/search/movie?api_key=42732b85a50a57a93e9344f93cee98d1&language=en-US&query={**s}&page=1&include_adult=false")]
        Task<Page> GetSearchResults(string s);
    }
}
