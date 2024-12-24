using PhishNet.Models;

namespace PhishNet.Console;

public class Cache
{
    public const string DirName = "cache";

    public Cache()
    {
        if (!Directory.Exists(DirName)) Directory.CreateDirectory(DirName);
        Artists = new ResourceCache<Artist>();
        Attendances = new ResourceCache<Attendance>();
        JamCharts = new ResourceCache<JamChart>();
        Reviews = new ResourceCache<Review>();
        SetlistItems = new ResourceCache<SetlistItem>();
        Shows = new ResourceCache<Show>();
        Songs = new ResourceCache<Song>();
        SongDatas = new ResourceCache<SongData>();
        Users = new ResourceCache<User>();
        Venues = new ResourceCache<Venue>();
    }

    public ResourceCache<Artist> Artists { get; }
    public ResourceCache<Attendance> Attendances { get; }
    public ResourceCache<JamChart> JamCharts { get; }
    public ResourceCache<Review> Reviews { get; }
    public ResourceCache<SetlistItem> SetlistItems { get; }
    public ResourceCache<Show> Shows { get; }
    public ResourceCache<Song> Songs { get; }
    public ResourceCache<SongData> SongDatas { get; }
    public ResourceCache<User> Users { get; }
    public ResourceCache<Venue> Venues { get; }

    public void Save()
    {
        Artists.Save();
        Attendances.Save();
        JamCharts.Save();
        Reviews.Save();
        SetlistItems.Save();
        Shows.Save();
        Songs.Save();
        SongDatas.Save();
        Users.Save();
        Venues.Save();
    }
}
