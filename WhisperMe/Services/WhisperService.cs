﻿using WhisperMe.Entities;
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
                    Color = whisperDto.Color,
                    CreatedDate = DateTime.Now
                };
                await _whisperRepository.SendWhisper(whisper);
            }
            else
            {
                throw new Exception("empty_whisper");
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
                throw new Exception("empty_whisper");
            }
        }

        public async Task<IEnumerable<Whisper>> ListWhispers(string jwt)
        {
            return await _whisperRepository.ListWhispers(jwt);
        }
    }
}
