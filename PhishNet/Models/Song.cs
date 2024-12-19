using System.Text.Json.Serialization;

namespace PhishNet.Models;

public class Song : IPhishNetApiResource
{
    [JsonPropertyName("songid")]
    public int Id { get; set; }

    [JsonPropertyName("song")]
    public string Name { get; set; }

    [JsonPropertyName("slug")]
    public string Slug { get; set; }

    [JsonPropertyName("abbr")]
    public string Abbr { get; set; }

    [JsonPropertyName("artist")]
    public string Artist { get; set; }

    [JsonPropertyName("debut")]
    public string Debut { get; set; }

    [JsonPropertyName("last_played")]
    public string LastPlayed { get; set; }

    [JsonPropertyName("times_played")]
    public int TimesPlayed { get; set; }

    [JsonPropertyName("last_permalink")]
    public Uri LastPermalink { get; set; }

    [JsonPropertyName("debut_permalink")]
    public Uri DebutPermalink { get; set; }

    [JsonPropertyName("gap")]
    public int Gap { get; set; }
}