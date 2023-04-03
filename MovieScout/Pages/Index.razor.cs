using Microsoft.AspNetCore.Components;
using MovieScout.Services;
using MovieScoutShared;

namespace MovieScout.Pages
{
    public partial class Index
    {
        public List<Movie> Movies { get; set; }
        [Inject]
        public UserInfoGlobalClass userGlobal { get; set; }
        [Inject]
        public IMovieDataService MovieDataService { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (userGlobal.Token == "")
                NavigationManager.NavigateTo("/login");
            try {
                Movies = await MovieDataService.GetTrendingMovies("day", userGlobal.Token);
            } catch (Exception ex) { }
        }

        async Task ChangeContentAsync(ChangeEventArgs e)
        {
            try { 

                Movies = await MovieDataService.GetTrendingMovies(e.Value.ToString(), userGlobal.Token);
            } catch (Exception ex) { }
        }
    }
}
