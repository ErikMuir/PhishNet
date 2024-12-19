using System.Text.Json.Serialization;

namespace PhishNet.Models;

public class SetlistItem : IPhishNetApiResource
{
    [JsonPropertyName("showid")]
    public long ShowId { get; set; }

    [JsonPropertyName("showdate")]
    public string ShowDate { get; set; }

    [JsonPropertyName("permalink")]
    public Uri Permalink { get; set; }

    [JsonPropertyName("showyear")]
    public string ShowYear { get; set; }

    [JsonPropertyName("uniqueid")]
    public int UniqueId { get; set; }

    [JsonPropertyName("meta")]
    public string Meta { get; set; }

    [JsonPropertyName("reviews")]
    public int Reviews { get; set; }

    [JsonPropertyName("exclude")]
    public int Exclude { get; set; }

    [JsonPropertyName("setlistnotes")]
    public string SetlistNotes { get; set; }

    [JsonPropertyName("soundcheck")]
    public string Soundcheck { get; set; }

    [JsonPropertyName("songid")]
    public int SongId { get; set; }

    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonPropertyName("transition")]
    public int Transition { get; set; }

    [JsonPropertyName("footnote")]
    public string Footnote { get; set; }

    [JsonPropertyName("set")]
    public string Set { get; set; }

    [JsonPropertyName("isjam")]
    public int IsJam { get; set; }

    [JsonPropertyName("isreprise")]
    public int IsReprise { get; set; }

    [JsonPropertyName("isjamchart")]
    public int IsJamChart { get; set; }

    [JsonPropertyName("jamchart_description")]
    public string JamChartDescription { get; set; }

    [JsonPropertyName("tracktime")]
    public string TrackTime { get; set; }

    [JsonPropertyName("gap")]
    public int Gap { get; set; }

    [JsonPropertyName("tourid")]
    public int TourId { get; set; }

    [JsonPropertyName("tourname")]
    public string TourName { get; set; }

    [JsonPropertyName("tourwhen")]
    public string TourWhen { get; set; }

    [JsonPropertyName("song")]
    public string Song { get; set; }

    [JsonPropertyName("nickname")]
    public string Nickname { get; set; }

    [JsonPropertyName("slug")]
    public string Slug { get; set; }

    [JsonPropertyName("is_original")]
    public int IsOriginal { get; set; }

    [JsonPropertyName("venueid")]
    public int VenueId { get; set; }

    [JsonPropertyName("venue")]
    public string Venue { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("trans_mark")]
    public string TransMark { get; set; }

    [JsonPropertyName("artistid")]
    public int ArtistId { get; set; }

    [JsonPropertyName("artist_slug")]
    public string ArtistSlug { get; set; }

    [JsonPropertyName("artist_name")]
    public string ArtistName { get; set; }
}