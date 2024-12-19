using System.Text.Json.Serialization;

namespace PhishNet.Models;

public class Venue : IPhishNetApiResource
{
    [JsonPropertyName("venueid")]
    public int Id { get; set; }

    [JsonPropertyName("venuename")]
    public string Name { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("venuenotes")]
    public string Notes { get; set; }

    [JsonPropertyName("alias")]
    public int Alias { get; set; }

    [JsonPropertyName("short_name")]
    public string ShortName { get; set; }
}