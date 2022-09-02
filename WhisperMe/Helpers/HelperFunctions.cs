using WhisperMe.ViewModels.Responses;

namespace WhisperMe.Helpers
{
    public static class HelperFunctions
    {
        public static Error ReturnErrorModel(string errorMessage)
        {
            throw new Error()
            {
                GeneralMessage = errorMessage,
                Code = "400"
            };

        }
    }
}
