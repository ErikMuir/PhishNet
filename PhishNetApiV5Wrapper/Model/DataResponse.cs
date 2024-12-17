using System.Text.Json.Serialization;

namespace PhishNetApiV5Wrapper.Model;

public class DataResponse<T> where T : class
{
    [JsonPropertyName("error")]
    public bool Error { get; set; }

    [JsonPropertyName("error_message")]
    public string ErrorMessage { get; set; }

    [JsonPropertyName("data")]
    public List<T> Data { get; set; }
}