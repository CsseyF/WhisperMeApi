using Microsoft.EntityFrameworkCore;
using WhisperMe.Database;
using WhisperMe.Entities;
using WhisperMe.Helpers;
using WhisperMe.Repository.Interfaces;

namespace WhisperMe.Repository
{
    public class WhisperRepository : IWhisperRepository
    {
        private readonly BaseContext _context;
        public WhisperRepository(BaseContext context)
        {
            _context = context;
        }
        public async Task SendWhisper(Whisper whisper)
        {
            _context.Whisper.Add(whisper);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWhisper(string whisperGuid)
        {
            var targetWhisper = _context.Whisper.FirstOrDefault(item => item.Guid.ToString() == whisperGuid);
            if (targetWhisper != null)
            {
                _context.Whisper.Remove(targetWhisper);
                await _context.SaveChangesAsync();
            }
            else
            {
                HelperFunctions.ReturnErrorModel("not_found_whisper");
            }

        }

        public async Task<IEnumerable<Whisper>> ListWhispers(string username)
        {
            var user = _context.User.FirstOrDefault(item => item.UserName == username);

            var list = _context.Whisper.Where(item => item.UserId == user.UserId).ToList();
            return list;
        }

    }
}
