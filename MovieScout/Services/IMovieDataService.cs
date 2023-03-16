using MovieScoutShared;
using Refit;

namespace MovieScout.Services
{
    public interface IMovieDataService
    {

        [Get("/trending/movie/day")]
        Task<Page> GetTrendingMovies([AliasAs("api_key")] string apikey);

        [Get("/movie/{id}")]
        Task<Movie>GetMovieDetails(int id, [AliasAs("api_key")] string apikey);

        [Get("/trending/movie/{**s}")]
        Task<Page> GetContent(string s, [AliasAs("api_key")] string apikey);

        [Get("/search/movie?language=en-US&include_adult=false")]
        Task<Page> GetSearchResults([AliasAs("query")] string s, [AliasAs("page")] int i, [AliasAs("api_key")] string apikey);
    }
}
