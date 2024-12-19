using System.Text.Json.Serialization;

namespace PhishNet.Models;

public class Artist : IPhishNetApiResource
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("artist")]
    public string Name { get; set; }

    [JsonPropertyName("slug")]
    public string Slug { get; set; }
}