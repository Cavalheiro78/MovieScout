using Microsoft.EntityFrameworkCore;
using MovieScout.Entities;

namespace MovieScout.DbContexts
{
    public class MovieContext : DbContext
    {
        public DbSet<MovieEntity> Movies { get; set; } = null!;

        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
            
        }
    }
}
