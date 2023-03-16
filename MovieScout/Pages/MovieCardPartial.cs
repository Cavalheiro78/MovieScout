using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MovieScout.Services;
using MovieScoutShared;
using System.Text.Json;

namespace MovieScout.Pages
{

    public partial class MovieCard
    {
        [Parameter]
        public Movie movie { get; set; }
        [Inject]
        public IMovieDataService MovieDataService { get; set; }
        [Inject]
        private IConfiguration configuration { get; set; }
        [Inject]
        private Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
        private string heartClass { get; set; }
        private List<Movie> movies { get; set; }
        protected override async Task OnInitializedAsync()
        {
            string json = (await localStorage.GetItemAsync<string>("movies"));
            movies = JsonSerializer.Deserialize<List<Movie>>(json);
            heartClass = (checkIfInFavouritesAsync(movie.id) ? "fa fa-heart" : "fa fa-heart-o");
        }

        async Task AddRemoveToFavoritesAsync(int id)
        {
            heartClass = (heartClass == "fa fa-heart") ? "fa fa-heart-o" : "fa fa-heart";
            try
            {
                Movie movie = await MovieDataService.GetMovieDetails(id, configuration["ApiKey"]);
                if (movie != null)
                {
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
            catch (Exception ex) { }
        }

        public bool checkIfInFavouritesAsync(int id)
        {
            if (movies != null)
                if (movies.Any(m => m.id == id))
                    return true;
                else
                    return false;
            else
                return false;
        }
    }
}
