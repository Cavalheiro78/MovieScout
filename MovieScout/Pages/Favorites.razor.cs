
using Microsoft.AspNetCore.Components;
using MovieScout.Services;
using MovieScoutShared;

namespace MovieScout.Pages
{
    public partial class Favorites
    {
        public List<Movie> movies { get; set; }
        public string json { get; set; }
        [Inject]
        public IMovieDataService MovieDataService { get; set; }
        [Inject]
        public UserInfoGlobalClass userGlobal { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            if (userGlobal.Token == "")
                NavigationManager.NavigateTo("/login");
            List<Movie> m = await MovieDataService.GetFavouritesMovies(userGlobal.Token, userGlobal.Id);
            if (m != null)
                movies = m;
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
    }
}
