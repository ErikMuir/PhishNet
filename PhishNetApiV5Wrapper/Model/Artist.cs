using System.Text.Json.Serialization;

namespace PhishNetApiV5Wrapper.Model;

public class Artist
{
    public int Id { get; set; }

    [JsonPropertyName("artist")]
    public string Name { get; set; }

    public string Slug { get; set; }
}