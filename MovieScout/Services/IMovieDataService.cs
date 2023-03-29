using Microsoft.AspNetCore.Cors;
using MovieScoutShared;
using Refit;

namespace MovieScout.Services
{
    public interface IMovieDataService
    {
        [Get("/movies/details")]
        Task<Movie>GetMovieDetails([AliasAs("id")] int id);

        [Get("/movies/trending/{**dayWeek}")]
        Task<List<Movie>> GetTrendingMovies(string dayWeek);

        [Get("/movies/query")]
        Task<Page> GetSearchResults([AliasAs("search")] string s, [AliasAs("pageNumber")] int i);

        [Get("/favourites")]
        Task<List<Movie>> GetFavouritesMovies();

        [Get("/favourites/{id}")]
        Task<Movie> GeFavouritesMovie(int id);

        [Post("/favourites")]
        Task AddMovie([Body]Movie movie);
        
        [Delete("/favourites/{id}")]
        Task DeleteMovie(int id);
    }
}
