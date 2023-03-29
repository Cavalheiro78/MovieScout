using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieScout.Services;
using MovieScoutShared;

namespace Movies.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        public IMovieDataService MovieDataService { get; set; }

        public MoviesController([FromServices]IMovieDataService movieDataService) 
        {
            MovieDataService = movieDataService;
        }

        [HttpGet("trending/{dayWeek}")]
        public async Task<ActionResult<List<Movie>>> GetTrendingMoviesDayAsync(string dayWeek)
        {
            Page page = await MovieDataService.GetTrendingMoviesDay(dayWeek);
            
            if (page != null)
                return Ok(page.results.ToList());

            return NotFound();
        }

        [HttpGet("details")]
        public async Task<ActionResult<Movie>> GetMovieDetails(int id)
        {
            Movie movie = await MovieDataService.GetMovieDetails(id);
            
            if (movie != null) 
                return Ok(movie);

            return NotFound();
        }

        [HttpGet("query")]
        public async Task<ActionResult<Page>> GetSearchResults(string search, int pageNumber)
        {
            if (pageNumber == null)
                return BadRequest();

            Page page = await MovieDataService.GetSearchResults(search, pageNumber);

            if (page != null) 
                return Ok(page);

            return NotFound();
        }
    }
}
