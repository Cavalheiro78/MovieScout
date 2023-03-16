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
        [Inject]
        private IConfiguration configuration { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try {

                Page page = await MovieDataService.GetTrendingMovies(configuration["ApiKey"]);
                if (page != null)
                    Movies = page.results.ToList();
                
            } catch (Exception ex) { }
        }

        async Task ChangeContentAsync(ChangeEventArgs e)
        {
            try { 

                Page page = await MovieDataService.GetContent(e.Value.ToString(), configuration["ApiKey"]);
                if (page != null)
                    Movies = page.results.ToList();

            } catch (Exception ex) { }
        }
    }
}
