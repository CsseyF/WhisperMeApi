using AutoMapper;
using WhisperMe.Entities;
using WhisperMe.ViewModels.Dtos;

namespace WhisperMe.Helpers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<Whisper, WhisperDTO>().ReverseMap();
        }
    }
}
