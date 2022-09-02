using Newtonsoft.Json;

namespace WhisperMe.ViewModels.Responses
{
    public class ExceptionVM : Exception
    {
        public ExceptionVM() : base() { }

        public List<ErrorRaw> errors { get; set; }

    }

    public class Error : Exception
    {
        [JsonProperty("general_message")]
        public string GeneralMessage { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }


    }

    [Serializable]
    public class ErrorRaw
    {
        public string general_message { get; set; }
        public string code { get; set; }
    }

}

