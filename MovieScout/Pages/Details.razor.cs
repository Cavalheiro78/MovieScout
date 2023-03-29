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
        private IConfiguration configuration { get; set; }
        protected override async Task OnInitializedAsync()
        {
            movie = await MovieDataService.GetMovieDetails(int.Parse(Id));

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
