using MovieScoutShared;
using System.Text.Json;

namespace MovieScout.Services
{
    public class MovieDataService : IMovieDataService
    {
        private readonly HttpClient _httpClient;
        private const string APIKEY = "?api_key=42732b85a50a57a93e9344f93cee98d1";
        public MovieDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        

        public async Task<IEnumerable<Movie>> GetTrendingMovies()
        {
            Page page = await JsonSerializer.DeserializeAsync<Page>
                    (await _httpClient.GetStreamAsync($"trending/movie/day{APIKEY}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = false });
            
            return page.results;
        }


        public async Task<IEnumerable<Movie>> GetContent(string s)
        {
            Page page = await JsonSerializer.DeserializeAsync<Page>
                    (await _httpClient.GetStreamAsync($"trending/movie/{s}{APIKEY}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = false });

            return page.results;
        }
        public async Task<Movie> GetMovieDetails(int id)
        {
                return await JsonSerializer.DeserializeAsync<Movie>
                    (await _httpClient.GetStreamAsync($"movie/{id}{APIKEY}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Movie>> GetSearchResults(string s)
        {
            Page page = await JsonSerializer.DeserializeAsync<Page>
                    (await _httpClient.GetStreamAsync($"search/movie{APIKEY}&language=en-US&query={s}&page=1&include_adult=false"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = false });

            return page.results;
        }
    }
}
