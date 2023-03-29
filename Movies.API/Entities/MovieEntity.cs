using MovieScoutShared;
using System.ComponentModel.DataAnnotations;

namespace MovieScout.Entities
{
    public class MovieEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Original_Title { get; set; }
        [Required]
        public string Overview { get; set; }
        [Required]
        public string Poster_Path { get; set; }
        [Required]
        public int Runtime { get; set; }
        [Required]
        public double Popularity { get; set; }
        [Required]
        public string Release_Date { get; set; }
        [Required]
        public string Tagline { get; set; }
        [Required]
        public double vote_average { get; set; }
        [Required]
        public int vote_count { get; set; }
    }
}
