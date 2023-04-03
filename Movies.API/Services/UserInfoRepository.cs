using MovieScout.DbContexts;
using MovieScout.Entities;

namespace Movies.API.Services
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly MovieContext _context;

        public UserInfoRepository(MovieContext context) 
        {
            _context = context;
        }
        public void AddUser(string username, string password, string email)
        {
            UserEntity user = new UserEntity(username, password, email);

            _context.Users.Add(user);

            _context.SaveChanges();
        }
    }
}
