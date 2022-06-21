using WhisperMe.Entities;

namespace WhisperMe.Repository.Interfaces
{
    public interface IWhisperRepository
    {
        public Task SendWhisper(Whisper whisper);
        public Task DeleteWhisper(string whisperGuid);
        public Task<IEnumerable<Whisper>> ListWhispers(string username);

    }
}
