using System.Text.Json.Serialization;

namespace JsonChallenge.Web.Models
{
    public class ApiReponse
    {
        [JsonPropertyOrder(-1)]
        public DateTimeOffset TimeStamp { get; set; }
        [JsonPropertyOrder(-1)]
        public long ExecutationTimeMs { get; set; }
    }
    public class ApiPostReponse : ApiReponse
    {
        public string? Message { get; set; }
        public int? Count { get; set; } = 0;
    }
    public class ApiResponseWithData<T> : ApiReponse
    {
        public T? Data { get; set; }

    }
}
