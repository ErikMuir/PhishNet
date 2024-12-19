using System.Text.Json.Serialization;

namespace PhishNetApiV5Wrapper.Models;

public class SongData : IPhishNetApiResource
{
    [JsonPropertyName("songid")]
    public int SongId { get; set; }

    [JsonPropertyName("song")]
    public string Name { get; set; }

    [JsonPropertyName("nickname")]
    public string Nickname { get; set; }

    [JsonPropertyName("slug")]
    public string Slug { get; set; }

    [JsonPropertyName("lyrics")]
    public string Lyrics { get; set; }

    [JsonPropertyName("history")]
    public string History { get; set; }

    [JsonPropertyName("historian")]
    public string Historian { get; set; }
}