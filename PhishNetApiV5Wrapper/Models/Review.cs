using System.Text.Json.Serialization;

namespace PhishNetApiV5Wrapper.Models;

public class Review : IPhishNetApiResource
{
    [JsonPropertyName("reviewid")]
    public long Id { get; set; }

    [JsonPropertyName("uid")]
    public long UserId { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("review_text")]
    public string ReviewText { get; set; }

    [JsonPropertyName("posted_at")]
    public string PostedAt { get; set; }

    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("showid")]
    public long ShowId { get; set; }

    [JsonPropertyName("showdate")]
    public string ShowDate { get; set; }

    [JsonPropertyName("showyear")]
    public string ShowYear { get; set; }

    [JsonPropertyName("permalink")]
    public Uri Permalink { get; set; }

    [JsonPropertyName("artistid")]
    public int ArtistId { get; set; }

    [JsonPropertyName("artist_name")]
    public string ArtistName { get; set; }

    [JsonPropertyName("venue")]
    public string Venue { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }
}