using Newtonsoft.Json;

namespace WhisperMe.ViewModels.Dtos
{
    public class UserDTO
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
