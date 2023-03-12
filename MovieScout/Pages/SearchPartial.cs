using Microsoft.AspNetCore.Components;
using MovieScout.Services;
using MovieScoutShared;
using System.Text.Json;

namespace MovieScout.Pages
{
    public partial class Search
    {
        public IEnumerable<Movie> movies { get; set; }
        [Inject]
        public IMovieDataService MovieDataService { get; set; }
        public string? searchValue { get; set; }
        public async Task SearchContentAsync() 
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                Page page = await MovieDataService.GetSearchResults(searchValue);
                movies = page.results;
            }
        }

        async Task AddRemoveToFavoritesAsync(int id)
        {
            Movie movie = await MovieDataService.GetMovieDetails(id);

            string json = (await localStorage.GetItemAsync<string>("movies"));
            List<Movie> movies;

            if (json != null)
            {
                movies = JsonSerializer.Deserialize<List<Movie>>(json);
                if (movies.Any(m => m.id == movie.id))
                    movies = movies.Where(m => m.id != movie.id).ToList();
                else
                    movies.Add(movie);
            }
            else
            {
                movies = new List<Movie>();
                movies.Add(movie);
            }

            await localStorage.RemoveItemAsync("movies");
            await localStorage.SetItemAsync("movies", movies);
        }
    }
}
