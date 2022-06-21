using Newtonsoft.Json;

namespace WhisperMe.ViewModels.Requests
{
    public class RemoveWhisperRequest
    {
        [JsonProperty("guid")]
        public string Guid { get; set; }
    }
}
