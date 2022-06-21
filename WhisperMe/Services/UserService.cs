using WhisperMe.Entities;
using WhisperMe.Repository.Interfaces;
using WhisperMe.Services.Interfaces;
using WhisperMe.ViewModels.Dtos;

namespace WhisperMe.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Register(UserDTO register)
        {

            if(_userRepository.GetUser(register.UserName) != 0)
            {
                throw new Exception("already_existent_username");
            }
            var newUser = new User()
            {
                Guid = Guid.NewGuid(),
                UserName = register.UserName,
                Password = register.Password,
            };

            await _userRepository.Register(newUser);
        }

        public async Task Login(UserDTO login)
        {
            var user = await _userRepository.Login(login.UserName, login.Password);
        }
    }
}
