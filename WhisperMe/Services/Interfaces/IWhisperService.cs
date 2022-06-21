using WhisperMe.Entities;
using WhisperMe.ViewModels.Dtos;

namespace WhisperMe.Services.Interfaces
{
    public interface IWhisperService
    {

        public Task SendWhisper(WhisperDTO whisperDto);
        public Task RemoveWhisper(string whisperGuid);
        public Task<IEnumerable<Whisper>> ListWhispers(string jwt);
    }
}