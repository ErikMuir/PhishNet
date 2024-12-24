using PhishNet.Models;

namespace PhishNet.Console;

public class PhishNetData : ICacheData
{
    public List<Artist> Artists { get; set; } = [];
    public List<Attendance> Attendances { get; set; } = [];
    public List<JamChart> JamCharts { get; set; } = [];
    public List<Review> Reviews { get; set; } = [];
    public List<SetlistItem> SetlistItems { get; set; } = [];
    public List<Show> Shows { get; set; } = [];
    public List<Song> Songs { get; set; } = [];
    public List<SongData> SongDatas { get; set; } = [];
    public List<User> Users { get; set; } = [];
    public List<Venue> Venues { get; set; } = [];
}
