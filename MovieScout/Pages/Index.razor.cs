using Microsoft.AspNetCore.Components;
using MovieScout.Services;
using MovieScoutShared;

namespace MovieScout.Pages
{
    public partial class Index
    {
        public List<Movie> Movies { get; set; }
        [Inject]
        public IMovieDataService MovieDataService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try {
                Movies = await MovieDataService.GetTrendingMovies("day");
            } catch (Exception ex) { }
        }

        async Task ChangeContentAsync(ChangeEventArgs e)
        {
            try { 

                Movies = await MovieDataService.GetTrendingMovies(e.Value.ToString());
            } catch (Exception ex) { }
        }
    }
}
