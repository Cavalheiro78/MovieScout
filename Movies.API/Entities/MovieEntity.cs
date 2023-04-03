using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieScout.Entities
{
    public class MovieEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KeyId { get; set; }
        [Required]
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
        [ForeignKey("UserId")]
        public int UserId { get; set; }
    }
}
