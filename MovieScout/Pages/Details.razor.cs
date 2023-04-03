using Microsoft.AspNetCore.Components;
using MovieScout.Services;
using MovieScoutShared;

namespace MovieScout.Pages
{
    public partial class Details
    {
        [Parameter]
        public string Id { get; set; }
        public Movie movie { get; set; }
        public string genres { get; set; }
        public string runtime { get; set; }
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

            movie = await MovieDataService.GetMovieDetails(int.Parse(Id), userGlobal.Token);

            for (int i = 0; i < movie.genres.Length; i++) {
                if (i + 1 == movie.genres.Length)
                    genres += movie.genres[i].Name;
                else
                    genres += movie.genres[i].Name + ", ";
            }

            TimeSpan mvMin = TimeSpan.FromMinutes(movie.runtime); 
            runtime = mvMin.ToString(@"hh\hmm\m");
        }
    }
}
