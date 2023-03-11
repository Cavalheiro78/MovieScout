using Microsoft.AspNetCore.Components;
using MovieScout.Services;
using MovieScoutShared;
using System.Text.Json;

namespace MovieScout.Pages
{
    public partial class Favorites
    {
        public List<Movie> movies { get; set; }
        public string json { get; set; }
        [Inject]
        public IMovieDataService MovieDataService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            json = await localStorage.GetItemAsync<string>("movies");
            if (json != null)
                movies = JsonSerializer.Deserialize<List<Movie>>(json);

        }

        public void ChangeContent(ChangeEventArgs e)
        {
            switch (e.Value.ToString())
            {
                case ("alphabetically"):
                    movies = movies.OrderBy(m => m.title).ToList();
                    break;
                case ("popularity"):
                    movies = movies.OrderByDescending(m => m.popularity).ToList();
                    break;
                case ("classification"):
                    movies = movies.OrderByDescending(m => m.vote_average).ToList();
                    break;
            }
        }

        async Task AddRemoveToFavoritesAsync(int id)
        {
            Movie movie = await MovieDataService.GetMovieDetails(id);

            json = await localStorage.GetItemAsync<string>("movies");

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

            await localStorage.SetItemAsync("movies", movies);
        }

        
    }
}
