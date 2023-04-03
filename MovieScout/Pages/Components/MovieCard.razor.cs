using Microsoft.AspNetCore.Components;
using MovieScout.Services;
using MovieScoutShared;
using System.Text.Json;

namespace MovieScout.Pages.Components
{

    public partial class MovieCard
    {
        [Parameter]
        public Movie movie { get; set; }
        [Parameter]
        public string heartClass { get; set; }
        [Inject]
        public IMovieDataService MovieDataService { get; set; }
        [Inject]
        public UserInfoGlobalClass userGlobal { get; set; }
        private List<Movie> movies { get; set; } = new List<Movie>();

        protected override async Task OnInitializedAsync()
        {
            List<Movie> m = await MovieDataService.GetFavouritesMovies(userGlobal.Token, userGlobal.Id);
            if (m != null)
                movies = m;

            heartClass = checkIfInFavouritesAsync(movie.id) ? "fa fa-heart" : "fa fa-heart-o";
        }

        async Task AddRemoveToFavoritesAsync(int id)
        {
            heartClass = heartClass == "fa fa-heart" ? "fa fa-heart-o" : "fa fa-heart";
            try
            {
                Movie movie = await MovieDataService.GetMovieDetails(id, userGlobal.Token);
                if (movie != null)
                {
                    if (movies != null)
                    {
                        if (movies.Any(m => m.id == movie.id))
                            MovieDataService.DeleteMovie(movie.id, userGlobal.Token, userGlobal.Id);
                        else
                            MovieDataService.AddMovie(movie, userGlobal.Token, userGlobal.Id);
                    }
                }
            }
            catch (Exception ex) { }
        }

        public bool checkIfInFavouritesAsync(int id)
        {
            if (movies != null)
                if (movies.Any(m => m.id == id && m.userid == userGlobal.Id))
                    return true;
                else
                    return false;
            else
                return false;
        }
    }
}
