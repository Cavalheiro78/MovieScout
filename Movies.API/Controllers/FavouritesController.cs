using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Services;
using MovieScout.Entities;
using MovieScoutShared;

namespace Movies.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/favourites")]
    public class FavouritesController : ControllerBase
    {
        private readonly IMovieInfoRepository _movies;
        public IMapper _mapper { get; }

        public FavouritesController(IMovieInfoRepository movie, IMapper mapper)
        {
            _movies = movie ?? throw new ArgumentNullException(nameof(movie));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesAsync()
        {
            var movies = await _movies.GetMoviesAsync();
            List<Movie> mappedMovies = new List<Movie>();
            if (movies != null)
            {
                foreach (var movie in movies)
                {
                    mappedMovies.Add(_mapper.Map<Movie>(movie));
                }
                return Ok(mappedMovies);
            }
                

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieAsync(int id)
        {
            var movie = await _movies.GetMovieAsync(id);

            if (movie != null)
                return Ok(_mapper.Map<Movie>(movie));

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovieAsync(Movie movie)
        {
            if (_movies.MovieExists(movie.id))
                return BadRequest();

            if (movie != null)
            {
                _movies.AddMovie(_mapper.Map<MovieEntity>(movie));
                return Ok();
            }
                

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovieAsync(int id)
        {
            if (!_movies.MovieExists(id))
                return NotFound();

            _movies.DeleteMovie(id);

            return Ok();
        }
    }
}
