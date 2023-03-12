using Microsoft.AspNetCore.Components;
using MovieScout.Services;
using MovieScoutShared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MovieScout.Pages
{
    public partial class Index
    {
        public IEnumerable<Movie> Movies { get; set; }
        [Inject]
        public IMovieDataService MovieDataService { get; set; }
        protected override async Task OnInitializedAsync()
        {   
            Page page = await MovieDataService.GetTrendingMovies();
            Movies = page.results.ToList();
        }

        async Task ChangeContentAsync(ChangeEventArgs e)
        {
            Page page = await MovieDataService.GetContent(e.Value.ToString());
            Movies = page.results.ToList();
        }

        async Task AddRemoveToFavoritesAsync(int id)
        {
            Movie movie = await MovieDataService.GetMovieDetails(id);

            string json = (await localStorage.GetItemAsync<string>("movies"));
            List<Movie> movies;

            if (json != null)
            {
                movies = JsonSerializer.Deserialize<List<Movie>>(json);
                if(movies.Any(m => m.id == movie.id))
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
