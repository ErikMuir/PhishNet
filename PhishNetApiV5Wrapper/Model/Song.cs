using System.Text.Json.Serialization;

namespace PhishNetApiV5Wrapper.Model;

public class Song
{
    [JsonPropertyName("songid")]
    public int Id { get; set; }

    [JsonPropertyName("song")]
    public string Name { get; set; }

    public string Slug { get; set; }

    public string Abbr { get; set; }

    public string Artist { get; set; }

    public DateOnly Debut { get; set; }

    [JsonPropertyName("last_played")]
    public DateOnly LastPlayed { get; set; }

    [JsonPropertyName("times_played")]
    public int TimesPlayed { get; set; }

    [JsonPropertyName("last_permalink")]
    public Uri LastPermalink { get; set; }

    [JsonPropertyName("debut_permalink")]
    public Uri DebutPermalink { get; set; }

    public int Gap { get; set; }
}