using Newtonsoft.Json;

namespace WhisperMe.ViewModels.Dtos
{
    public class WhisperDTO
    {
        [JsonProperty("receiverUsername")]
        public string? ReceiverUsername { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("color")]
        public string? Color { get; set; }

        [JsonProperty("profilePicture")]
        public string ProfilePicture { get; set; }
    }
}
