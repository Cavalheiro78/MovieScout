using MovieScout.Entities;

namespace Movies.API.Services
{
    public interface IUserInfoRepository
    {
        void AddUser(string username, string password, string email);
    }
}
