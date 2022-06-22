using Microsoft.EntityFrameworkCore;
using WhisperMe.Database;
using WhisperMe.Entities;
using WhisperMe.Repository.Interfaces;

namespace WhisperMe.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BaseContext _userContext;
        public UserRepository(BaseContext userContext)
        {
            _userContext = userContext;
        }

        public async Task Register(User newUser)
        {

            _userContext.User.Add(newUser);
            await _userContext.SaveChangesAsync();

        }

        public async Task<User> Login(string username, string password)
        {
            var userReturn = await _userContext.User.FirstOrDefaultAsync(user => user.UserName == username && user.Password == password);

            if (userReturn != null)
            {
                return userReturn;
            }
            else
            {
                throw new Exception("not_found_user");
            }

        }

        public int GetUser(string username)
        {
            var user = _userContext.User.FirstOrDefault(user => user.UserName == username);
            if (user == null)
            {
                return 0;
            }
            else
            {
                return user.UserId;
            }

        }

    }
}
