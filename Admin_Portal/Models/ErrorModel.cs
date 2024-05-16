using System.Text.Json.Serialization;

namespace Bank_Portal.Models
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        [JsonIgnore]
        public Exception Exception { get; set; }
    }
}
