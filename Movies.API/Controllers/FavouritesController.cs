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
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesAsync(int userid)
        {
            IEnumerable<MovieEntity> movies = await _movies.GetMoviesAsync();
            IEnumerable<MovieEntity> userMovies = movies.Where(m => m.UserId == userid);
            List<Movie> mappedMovies = new List<Movie>();
            if (userMovies != null)
            {
                foreach (var movie in userMovies)
                {
                    mappedMovies.Add(_mapper.Map<Movie>(movie));
                }
                return Ok(mappedMovies);
            }
                

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieAsync(int id, int userid)
        {
            var movie = await _movies.GetMovieAsync(id, userid);

            if (movie != null)
                return Ok(_mapper.Map<Movie>(movie));

            return NotFound();
        }

        [HttpPost()]
        public async Task<ActionResult<Movie>> AddMovieAsync(Movie movie, int userid)
        {
            if (_movies.MovieExists(movie.id, userid))
                return BadRequest();

            if (movie != null)
            {
                MovieEntity movieEntity = _mapper.Map<MovieEntity>(movie);
                movieEntity.UserId = userid;
                _movies.AddMovie(movieEntity);
                return Ok();
            }
                

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovieAsync(int id, int userid)
        {
            if (!_movies.MovieExists(id, userid))
                return NotFound();

            _movies.DeleteMovie(id, userid);

            return Ok();
        }
    }
}
