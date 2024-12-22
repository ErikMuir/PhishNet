using PhishNet.Models;
using System.Text.Json;

namespace PhishNet.Console;

public class DataCache
{
    public DataCache()
    {
        if (!Directory.Exists("cache"))
            Directory.CreateDirectory("cache");
        if (!File.Exists("cache/artists.json"))
            SaveArtists();
        if (!File.Exists("cache/attendance.json"))
            SaveAttendance();
        if (!File.Exists("cache/jamcharts.json"))
            SaveJamCharts();
        if (!File.Exists("cache/reviews.json"))
            SaveReviews();
        if (!File.Exists("cache/setlistitems.json"))
            SaveSetlistItems();
        if (!File.Exists("cache/shows.json"))
            SaveShows();
        if (!File.Exists("cache/songs.json"))
            SaveSongs();
        if (!File.Exists("cache/songdata.json"))
            SaveSongData();
        if (!File.Exists("cache/users.json"))
            SaveUsers();
        if (!File.Exists("cache/venues.json"))
            SaveVenues();
    }

    public Dictionary<string, List<Artist>> Artists { get; set; } = new();
    public Dictionary<string, List<Attendance>> Attendance { get; set; } = new();
    public Dictionary<string, List<JamChart>> JamCharts { get; set; } = new();
    public Dictionary<string, List<Review>> Reviews { get; set; } = new();
    public Dictionary<string, List<SetlistItem>> SetlistItems { get; set; } = new();
    public Dictionary<string, List<Show>> Shows { get; set; } = new();
    public Dictionary<string, List<SongData>> SongData { get; set; } = new();
    public Dictionary<string, List<Song>> Songs { get; set; } = new();
    public Dictionary<string, List<User>> Users { get; set; } = new();
    public Dictionary<string, List<Venue>> Venues { get; set; } = new();

    #region Save Methods

    public void SaveArtists()
    {
        File.WriteAllText("cache/artists.json", JsonSerializer.Serialize(Artists));
    }

    public void SaveAttendance()
    {
        File.WriteAllText("cache/attendance.json", JsonSerializer.Serialize(Attendance));
    }

    public void SaveJamCharts()
    {
        File.WriteAllText("cache/jamcharts.json", JsonSerializer.Serialize(JamCharts));
    }

    public void SaveReviews()
    {
        File.WriteAllText("cache/reviews.json", JsonSerializer.Serialize(Reviews));
    }

    public void SaveSetlistItems()
    {
        File.WriteAllText("cache/setlistitems.json", JsonSerializer.Serialize(SetlistItems));
    }

    public void SaveShows()
    {
        File.WriteAllText("cache/shows.json", JsonSerializer.Serialize(Shows));
    }

    public void SaveSongs()
    {
        File.WriteAllText("cache/songs.json", JsonSerializer.Serialize(Songs));
    }

    public void SaveSongData()
    {
        File.WriteAllText("cache/songdata.json", JsonSerializer.Serialize(SongData));
    }

    public void SaveUsers()
    {
        File.WriteAllText("cache/users.json", JsonSerializer.Serialize(Users));
    }

    public void SaveVenues()
    {
        File.WriteAllText("cache/venues.json", JsonSerializer.Serialize(Venues));
    }

    public void SaveAll()
    {
        SaveArtists();
        SaveAttendance();
        SaveJamCharts();
        SaveReviews();
        SaveSetlistItems();
        SaveShows();
        SaveSongData();
        SaveSongs();
        SaveUsers();
        SaveVenues();
    }

    #endregion

    #region Clear Methods

    public void ClearArtists()
    {
        Artists.Clear();
    }

    public void ClearAttendance()
    {
        Attendance.Clear();
    }

    public void ClearJamCharts()
    {
        JamCharts.Clear();
    }

    public void ClearReviews()
    {
        Reviews.Clear();
    }

    public void ClearSetlistItems()
    {
        SetlistItems.Clear();
    }

    public void ClearShows()
    {
        Shows.Clear();
    }

    public void ClearSongs()
    {
        Songs.Clear();
    }

    public void ClearSongData()
    {
        SongData.Clear();
    }

    public void ClearUsers()
    {
        Users.Clear();
    }

    public void ClearVenues()
    {
        Venues.Clear();
    }

    public void ClearAll()
    {
        SaveArtists();
        SaveAttendance();
        SaveJamCharts();
        SaveReviews();
        SaveSetlistItems();
        SaveShows();
        SaveSongData();
        SaveSongs();
        SaveUsers();
        SaveVenues();
    }

    #endregion

    #region Load Methods

    public void LoadArtists()
    {
        try
        {
            if (!File.Exists("cache/artists.json")) return;

            var json = File.ReadAllText("cache/artists.json");
            if (string.IsNullOrWhiteSpace(json)) return;

            var artists = JsonSerializer.Deserialize<Dictionary<string, List<Artist>>>(json);
            if (artists is null) return;

            Artists = artists;
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error loading artists: {e.Message}");
        }
    }

    public void LoadAttendance()
    {
        try
        {
            if (!File.Exists("cache/attendance.json")) return;

            var json = File.ReadAllText("cache/attendance.json");
            if (string.IsNullOrWhiteSpace(json)) return;

            var attendance = JsonSerializer.Deserialize<Dictionary<string, List<Attendance>>>(json);
            if (attendance is null) return;

            Attendance = attendance;
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error loading attendance: {e.Message}");
        }
    }

    public void LoadJamCharts()
    {
        try
        {
            if (!File.Exists("cache/jamcharts.json")) return;

            var json = File.ReadAllText("cache/jamcharts.json");
            if (string.IsNullOrWhiteSpace(json)) return;

            var jamCharts = JsonSerializer.Deserialize<Dictionary<string, List<JamChart>>>(json);
            if (jamCharts is null) return;

            JamCharts = jamCharts;
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error loading jamcharts: {e.Message}");
        }
    }

    public void LoadReviews()
    {
        try
        {
            if (!File.Exists("cache/reviews.json")) return;

            var json = File.ReadAllText("cache/reviews.json");
            if (string.IsNullOrWhiteSpace(json)) return;

            var reviews = JsonSerializer.Deserialize<Dictionary<string, List<Review>>>(json);
            if (reviews is null) return;

            Reviews = reviews;
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error loading reviews: {e.Message}");
        }
    }

    public void LoadSetlistItems()
    {
        try
        {
            if (!File.Exists("cache/setlistitems.json")) return;

            var json = File.ReadAllText("cache/setlistitems.json");
            if (string.IsNullOrWhiteSpace(json)) return;

            var setlistItems = JsonSerializer.Deserialize<Dictionary<string, List<SetlistItem>>>(json);
            if (setlistItems is null) return;

            SetlistItems = setlistItems;
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error loading setlistitems: {e.Message}");
        }
    }

    public void LoadShows()
    {
        try
        {
            if (!File.Exists("cache/shows.json")) return;

            var json = File.ReadAllText("cache/shows.json");
            if (string.IsNullOrWhiteSpace(json)) return;

            var shows = JsonSerializer.Deserialize<Dictionary<string, List<Show>>>(json);
            if (shows is null) return;

            Shows = shows;
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error loading shows: {e.Message}");
        }
    }

    public void LoadSongs()
    {
        try
        {
            if (!File.Exists("cache/songs.json")) return;

            var json = File.ReadAllText("cache/songs.json");
            if (string.IsNullOrWhiteSpace(json)) return;

            var songs = JsonSerializer.Deserialize<Dictionary<string, List<Song>>>(json);
            if (songs is null) return;

            Songs = songs;
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error loading songs: {e.Message}");
        }
    }

    public void LoadSongData()
    {
        try
        {
            if (!File.Exists("cache/songdata.json")) return;

            var json = File.ReadAllText("cache/songdata.json");
            if (string.IsNullOrWhiteSpace(json)) return;

            var songdata = JsonSerializer.Deserialize<Dictionary<string, List<SongData>>>(json);
            if (songdata is null) return;

            SongData = songdata;
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error loading songdata: {e.Message}");
        }
    }

    public void LoadUsers()
    {
        try
        {
            if (!File.Exists("cache/users.json")) return;

            var json = File.ReadAllText("cache/users.json");
            if (string.IsNullOrWhiteSpace(json)) return;

            var users = JsonSerializer.Deserialize<Dictionary<string, List<User>>>(json);
            if (users is null) return;

            Users = users;
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error loading users: {e.Message}");
        }
    }

    public void LoadVenues()
    {
        try
        {
            if (!File.Exists("cache/venues.json")) return;

            var json = File.ReadAllText("cache/venues.json");
            if (string.IsNullOrWhiteSpace(json)) return;

            var venues = JsonSerializer.Deserialize<Dictionary<string, List<Venue>>>(json);
            if (venues is null) return;

            Venues = venues;
        }
        catch (Exception e)
        {
            System.Console.WriteLine($"Error loading venues: {e.Message}");
        }
    }

    public void LoadAll()
    {
        LoadArtists();
        LoadAttendance();
        LoadJamCharts();
        LoadReviews();
        LoadSetlistItems();
        LoadShows();
        LoadSongData();
        LoadSongs();
        LoadUsers();
        LoadVenues();
    }

    #endregion
}