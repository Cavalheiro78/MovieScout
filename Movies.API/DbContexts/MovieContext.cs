using Microsoft.EntityFrameworkCore;
using MovieScout.Entities;

namespace MovieScout.DbContexts
{
    public class MovieContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<MovieEntity> Movies { get; set; } = null!;

        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
            
        }
    }
}
