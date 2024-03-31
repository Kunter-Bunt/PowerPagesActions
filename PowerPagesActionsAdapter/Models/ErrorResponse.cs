using Newtonsoft.Json;

namespace PowerPagesActionsAdapter.Models
{
    public class ErrorResponse
    {
        public ErrorResponse(string message)
        {
            Message = message;
        }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
