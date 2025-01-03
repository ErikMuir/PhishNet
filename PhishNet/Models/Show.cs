using System.Text.Json.Serialization;

namespace PhishNet.Models;

public class Show : IPhishNetApiResource
{
    [JsonPropertyName("showid")]
    public long Id { get; set; }

    [JsonPropertyName("showyear")]
    public string ShowYear { get; set; }

    [JsonPropertyName("showmonth")]
    public int ShowMonth { get; set; }

    [JsonPropertyName("showday")]
    public int ShowDay { get; set; }

    [JsonPropertyName("showdate")]
    public string ShowDate { get; set; }

    [JsonPropertyName("permalink")]
    public Uri Permalink { get; set; }

    [JsonPropertyName("exclude_from_stats")]
    public int ExcludeFromStats { get; set; }

    [JsonPropertyName("venueid")]
    public int? VenueId { get; set; }

    [JsonPropertyName("setlist_notes")]
    public string SetlistNotes { get; set; }

    [JsonPropertyName("venue")]
    public string Venue { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("artistid")]
    public int? ArtistId { get; set; }

    [JsonPropertyName("artist_name")]
    public string ArtistName { get; set; }

    [JsonPropertyName("tourid")]
    public int? TourId { get; set; }

    [JsonPropertyName("tour_name")]
    public string TourName { get; set; }

    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public string UpdatedAt { get; set; }
}