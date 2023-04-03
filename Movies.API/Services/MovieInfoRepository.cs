using Microsoft.EntityFrameworkCore;
using MovieScout.DbContexts;
using MovieScout.Entities;

namespace Movies.API.Services
{
    public class MovieInfoRepository : IMovieInfoRepository
    {
        private readonly MovieContext _context;

        public MovieInfoRepository(MovieContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddMovie(MovieEntity movie)
        {
            if (movie != null)
                _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public void DeleteMovie(int id, int userid)
        {
            MovieEntity movie = _context.Movies.Where(m => m.Id == id && m.UserId == userid).FirstOrDefault();
            if (movie != null)
                _context.Movies.Remove(movie);
            _context.SaveChanges();
        }

        public async Task<MovieEntity> GetMovieAsync(int id, int userid)
        {
            return await _context.Movies.Where(m => m.Id == id && m.UserId == userid).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MovieEntity>> GetMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public bool MovieExists(int id, int userid)
        {
            foreach (var movie in _context.Movies)
                if (movie.Id == id && movie.UserId == userid)
                    return true;

            return false;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
