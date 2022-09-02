using System.IdentityModel.Tokens.Jwt;
using WhisperMe.Entities;
using WhisperMe.Helpers;
using WhisperMe.Repository.Interfaces;
using WhisperMe.Services.Interfaces;
using WhisperMe.ViewModels.Dtos;

namespace WhisperMe.Services
{
    public class WhisperService : IWhisperService
    {
        private readonly IWhisperRepository _whisperRepository;
        private readonly IUserRepository _userRepository;
        public WhisperService(IWhisperRepository whisperRepository, IUserRepository userRepository)
        {
            _whisperRepository = whisperRepository;
            _userRepository = userRepository;
        }

        public async Task SendWhisper(WhisperDTO whisperDto)
        {
            if (whisperDto != null)
            {
                var whisper = new Whisper()
                {
                    UserId = _userRepository.GetUser(whisperDto.ReceiverUsername),
                    Guid = Guid.NewGuid(),
                    Message = whisperDto.Message,
                    ProfilePicture = whisperDto.ProfilePicture,
                    Color = whisperDto.Color,
                    CreatedDate = DateTime.Now
                };
                await _whisperRepository.SendWhisper(whisper);
            }
            else
            {
                HelperFunctions.ReturnErrorModel("empty_whisper");
            }

        }

        public async Task RemoveWhisper(string whisperGuid)
        {
            if(!String.IsNullOrEmpty(whisperGuid))
            {
                await _whisperRepository.DeleteWhisper(whisperGuid);
            }
            else
            {
                HelperFunctions.ReturnErrorModel("empty_whisper");
            }
        }

        public async Task<IEnumerable<Whisper>> ListWhispers(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(jwt);
            var claim = jwtSecurityToken.Claims.First().Value;

            var list = await _whisperRepository.ListWhispers(claim);

            if (!list.Any())
            {
                HelperFunctions.ReturnErrorModel("not_found_whispers");
            }

            return list;
        }
    }
}
