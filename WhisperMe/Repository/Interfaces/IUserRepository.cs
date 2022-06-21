using WhisperMe.Entities;
using WhisperMe.ViewModels.Dtos;

namespace WhisperMe.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task Register(User user);
        public Task<User> Login(string username, string password);
        public int GetUser(string username);
    }
}
