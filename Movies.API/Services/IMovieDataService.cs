using MovieScoutShared;
using Refit;

namespace MovieScout.Services
{
    public interface IMovieDataService
    {
        [Get("/movie/{id}")]
        Task<Movie>GetMovieDetails(int id);

        [Get("/trending/movie/{**dayWeek}")]
        Task<Page> GetTrendingMoviesDay(string dayWeek);

        [Get("/search/movie?language=en-US&include_adult=false")]
        Task<Page> GetSearchResults([AliasAs("query")] string s, [AliasAs("page")] int i);
    }
}
