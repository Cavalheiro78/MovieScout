using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieScout.Entities
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        public ICollection<MovieEntity> Favourites { get; set;} = new List<MovieEntity>();

        public UserEntity(string username, string password, string email) 
        {
            Username = username;
            Password = password;
            Email = email;
        }
    }
}
