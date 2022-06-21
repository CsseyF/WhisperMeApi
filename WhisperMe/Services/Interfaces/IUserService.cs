using WhisperMe.ViewModels.Dtos;

namespace WhisperMe.Services.Interfaces
{
    public interface IUserService
    {
        public Task Register(UserDTO userDto);
        public Task Login(UserDTO userDto);
    }
}
