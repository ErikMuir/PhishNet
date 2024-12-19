using System.Text.Json.Serialization;

namespace PhishNetApiV5Wrapper.Models;

public class User : IPhishNetApiResource
{
    [JsonPropertyName("uid")]
    public long Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("date_joined")]
    public string DateJoined { get; set; }
}