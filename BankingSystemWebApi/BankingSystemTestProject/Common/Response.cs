using Newtonsoft.Json;

namespace BankingSystemTestProject.Common
{
    public class Response
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
