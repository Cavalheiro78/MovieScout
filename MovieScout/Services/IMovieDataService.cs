using Microsoft.AspNetCore.Cors;
using MovieScoutShared;
using Refit;

namespace MovieScout.Services
{
    public interface IMovieDataService
    {
        [Get("/movies/details")]
        Task<Movie>GetMovieDetails([AliasAs("id")] int id, [Authorize("Bearer")] string authorization);

        [Get("/movies/trending/{**dayWeek}")]
        Task<List<Movie>> GetTrendingMovies(string dayWeek, [Authorize("Bearer")] string authorization);

        [Get("/movies/query")]
        Task<Page> GetSearchResults([AliasAs("search")] string s, [AliasAs("pageNumber")] int i, [Authorize("Bearer")] string authorization);

        [Get("/favourites")]
        Task<List<Movie>> GetFavouritesMovies([Authorize("Bearer")] string authorization, int userid);

        [Get("/favourites/{id}")]
        Task<Movie> GeFavouritesMovie(int id, [Authorize("Bearer")] string authorization, int userid);

        [Post("/favourites")]
        Task AddMovie([Body]Movie movie, [Authorize("Bearer")] string authorization, int userid);
        
        [Delete("/favourites/{id}")]
        Task DeleteMovie(int id, [Authorize("Bearer")] string authorization, int userid);

        [Get("/users/userid")]
        Task<string> GetUserId(string username);

        [Post("/users/authenticate")]
        Task<string> Authenticate(string username, string password);

        [Post("/users/register")]
        Task RegisterUser(string username, string password, string email);
    }
}
