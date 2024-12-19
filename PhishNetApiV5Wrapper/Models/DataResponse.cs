using System.Text.Json.Serialization;

namespace PhishNetApiV5Wrapper.Models;

public class DataResponse<T> where T : IPhishNetApiResource
{
    [JsonPropertyName("error")]
    public object Error { get; set; }

    [JsonPropertyName("error_message")]
    public string ErrorMessage { get; set; }

    [JsonPropertyName("data")]
    public List<T> Data { get; set; }

    public bool IsError => Error is bool and true or int and not 0;

    public int? ErrorCode => IsError ? (int)Error : null;
}