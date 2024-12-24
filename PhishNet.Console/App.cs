using System.Text.Json;
using PhishNet.Models;

namespace PhishNet.Console;

public class App
{
    private readonly string _resource;
    private readonly string[] _queryOptions;
    private readonly bool _showHelp;
    private readonly PhishNetApiClient _apiClient;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly Cache<PhishNetData> _cache;
    private const string Help = """
        Usage:  dotnet run <resource> [id | <column> <value>]
        Resources:
            artists     [id]
            shows       [id]
            songs       [id]
            songdata    [id]
            venues      [id]
            users       <uid|username>                  <value>
            attendance  <uid|username|showid|showdate>  <value>
            reviews     <uid|username|showid|showdate>  <value>
            setlists    <slug|showid|showdate>          <value>
            jamcharts   <slug|showid|showdate>          <value>
        """;

    public App(string[] args)
    {
        _resource = args.FirstOrDefault();
        _queryOptions = args.Skip(1).ToArray();
        _showHelp = args.Contains("-h") || args.Contains("--help");
        _apiClient = new PhishNetApiClient();
        _serializerOptions = new JsonSerializerOptions { WriteIndented = true };
        _cache = new Cache<PhishNetData>();

        if (string.IsNullOrWhiteSpace(_resource))
            throw new PhishNetConsoleException("Missing required resource.");
    }

    public async Task Run()
    {
        if (_showHelp)
        {
            Log(Help);
            return;
        }

        try
        {
            var result = _resource switch
            {
                "artists" => await Artists(),
                "attendance" => await Attendance(),
                "jamcharts" => await JamCharts(),
                "reviews" => await Reviews(),
                "shows" => await Shows(),
                "songs" => await Songs(),
                "songdata" => await SongDatas(),
                "setlists" => await Setlists(),
                "users" => await Users(),
                "venues" => await Venues(),
                _ => throw new PhishNetConsoleException($"Invalid resource '{_resource}'.")
            };

            Log(result != null ? JsonSerializer.Serialize(result, _serializerOptions) : "No results found.");
        }
        catch (PhishNetConsoleException e)
        {
            Log($"Error: {e.Message}\nUse 'dotnet run -- --help' for a list of available resources and options.");
        }
        catch (Exception e)
        {
            Log(e.ToString());
        }
        finally
        {
            _cache.Save();
        }
    }

    #region Resource Actions

    private async Task<object> Artists()
    {
        if (_cache.Data.Artists.Count == 0)
        {
            _cache.Data.Artists = await _apiClient.GetArtistsAsync();
            _cache.Save();
        }

        if (_queryOptions.Length == 0)
            return _cache.Data.Artists;

        if (!int.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid artist ID '{_queryOptions[0]}'.");

        var artist = _cache.Data.Artists.SingleOrDefault(a => a.Id == id);

        if (artist == null)
        {
            artist = await _apiClient.GetArtistByIdAsync(id);
            if (artist != null)
            {
                _cache.Data.Artists.Add(artist);
                _cache.Save();
            }
        }

        return artist;
    }

    private async Task<object> Attendance()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        List<Attendance> attendances;

        switch (_queryOptions[0])
        {
            case "uid":
                if (!int.TryParse(_queryOptions[1], out var userId))
                    throw new PhishNetConsoleException($"Invalid user ID '{_queryOptions[1]}'.");
                attendances = _cache.Data.Attendances.Where(a => a.UserId == userId).ToList();
                if (attendances.Count == 0)
                {
                    attendances = await _apiClient.GetAttendanceByUserIdAsync(userId);
                    _cache.Data.Attendances.AddRange(attendances);
                    _cache.Save();
                }
                break;
            case "username":
                var username = _queryOptions[1];
                attendances = _cache.Data.Attendances.Where(a => a.Username == username).ToList();
                if (attendances.Count == 0)
                {
                    attendances = await _apiClient.GetAttendanceByUsernameAsync(username);
                    _cache.Data.Attendances.AddRange(attendances);
                    _cache.Save();
                }
                break;
            case "showid":
                if (!long.TryParse(_queryOptions[1], out var showId))
                    throw new PhishNetConsoleException($"Invalid show ID '{_queryOptions[1]}'.");
                attendances = _cache.Data.Attendances.Where(a => a.ShowId == showId).ToList();
                if (attendances.Count == 0)
                {
                    attendances = await _apiClient.GetAttendanceByShowIdAsync(showId);
                    _cache.Data.Attendances.AddRange(attendances);
                    _cache.Save();
                }
                break;
            case "showdate":
                if (!DateOnly.TryParse(_queryOptions[1], out var showDate))
                    throw new PhishNetConsoleException($"Invalid show date '{_queryOptions[1]}'.");
                attendances = _cache.Data.Attendances.Where(a => a.ShowDate.ToDateOnly() == showDate).ToList();
                if (attendances.Count == 0)
                {
                    attendances = await _apiClient.GetAttendanceByShowDateAsync(showDate);
                    _cache.Data.Attendances.AddRange(attendances);
                    _cache.Save();
                }
                break;
            default:
                throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.");
        }

        return attendances;
    }

    private async Task<object> JamCharts()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        List<JamChart> jamCharts;

        switch (_queryOptions[0])
        {
            case "slug":
                var songSlug = _queryOptions[1];
                jamCharts = _cache.Data.JamCharts.Where(j => j.Slug == songSlug).ToList();
                if (jamCharts.Count == 0)
                {
                    jamCharts = await _apiClient.GetJamChartsBySongAsync(songSlug);
                    _cache.Data.JamCharts.AddRange(jamCharts);
                    _cache.Save();
                }
                break;
            case "showid":
                if (!long.TryParse(_queryOptions[1], out var showId))
                    throw new PhishNetConsoleException($"Invalid show ID '{_queryOptions[1]}'.");
                jamCharts = _cache.Data.JamCharts.Where(j => j.ShowId == showId).ToList();
                if (jamCharts.Count == 0)
                {
                    jamCharts = await _apiClient.GetJamChartsByShowIdAsync(showId);
                    _cache.Data.JamCharts.AddRange(jamCharts);
                    _cache.Save();
                }
                break;
            case "showdate":
                if (!DateOnly.TryParse(_queryOptions[1], out var showDate))
                    throw new PhishNetConsoleException($"Invalid show date '{_queryOptions[1]}'.");
                jamCharts = _cache.Data.JamCharts.Where(j => j.ShowDate.ToDateOnly() == showDate).ToList();
                if (jamCharts.Count == 0)
                {
                    jamCharts = await _apiClient.GetJamChartsByShowDateAsync(showDate);
                    _cache.Data.JamCharts.AddRange(jamCharts);
                    _cache.Save();
                }
                break;
            default:
                throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.");
        }

        return jamCharts;
    }

    private async Task<object> Reviews()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        List<Review> reviews;

        switch (_queryOptions[0])
        {
            case "uid":
                if (!int.TryParse(_queryOptions[1], out var userId))
                    throw new PhishNetConsoleException($"Invalid user ID '{_queryOptions[1]}'.");
                reviews = _cache.Data.Reviews.Where(a => a.UserId == userId).ToList();
                if (reviews.Count == 0)
                {
                    reviews = await _apiClient.GetReviewsByUserIdAsync(userId);
                    _cache.Data.Reviews.AddRange(reviews);
                    _cache.Save();
                }
                break;
            case "username":
                var username = _queryOptions[1];
                reviews = _cache.Data.Reviews.Where(a => a.Username == username).ToList();
                if (reviews.Count == 0)
                {
                    reviews = await _apiClient.GetReviewsByUsernameAsync(username);
                    _cache.Data.Reviews.AddRange(reviews);
                    _cache.Save();
                }
                break;
            case "showid":
                if (!long.TryParse(_queryOptions[1], out var showId))
                    throw new PhishNetConsoleException($"Invalid show ID '{_queryOptions[1]}'.");
                reviews = _cache.Data.Reviews.Where(a => a.ShowId == showId).ToList();
                if (reviews.Count == 0)
                {
                    reviews = await _apiClient.GetReviewsByShowIdAsync(showId);
                    _cache.Data.Reviews.AddRange(reviews);
                    _cache.Save();
                }
                break;
            case "showdate":
                if (!DateOnly.TryParse(_queryOptions[1], out var showDate))
                    throw new PhishNetConsoleException($"Invalid show date '{_queryOptions[1]}'.");
                reviews = _cache.Data.Reviews.Where(a => a.ShowDate.ToDateOnly() == showDate).ToList();
                if (reviews.Count == 0)
                {
                    reviews = await _apiClient.GetReviewsByShowDateAsync(showDate);
                    _cache.Data.Reviews.AddRange(reviews);
                    _cache.Save();
                }
                break;
            default:
                throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.");
        }

        return reviews;
    }

    private async Task<object> Setlists()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        List<SetlistItem> setlistItems;

        switch (_queryOptions[0])
        {
            case "slug":
                var songSlug = _queryOptions[1];
                setlistItems = _cache.Data.SetlistItems.Where(j => j.Slug == songSlug).ToList();
                if (setlistItems.Count == 0)
                {
                    setlistItems = await _apiClient.GetSongPerformancesAsync(songSlug);
                    _cache.Data.SetlistItems.AddRange(setlistItems);
                    _cache.Save();
                }
                break;
            case "showid":
                if (!long.TryParse(_queryOptions[1], out var showId))
                    throw new PhishNetConsoleException($"Invalid show ID '{_queryOptions[1]}'.");
                setlistItems = _cache.Data.SetlistItems.Where(j => j.ShowId == showId).ToList();
                if (setlistItems.Count == 0)
                {
                    setlistItems = await _apiClient.GetSetlistByShowIdAsync(showId);
                    _cache.Data.SetlistItems.AddRange(setlistItems);
                    _cache.Save();
                }
                break;
            case "showdate":
                if (!DateOnly.TryParse(_queryOptions[1], out var showDate))
                    throw new PhishNetConsoleException($"Invalid show date '{_queryOptions[1]}'.");
                setlistItems = _cache.Data.SetlistItems.Where(j => j.ShowDate.ToDateOnly() == showDate).ToList();
                if (setlistItems.Count == 0)
                {
                    setlistItems = await _apiClient.GetSetlistByShowDateAsync(showDate);
                    _cache.Data.SetlistItems.AddRange(setlistItems);
                    _cache.Save();
                }
                break;
            default:
                throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.");
        }

        return setlistItems;
    }

    private async Task<object> Shows()
    {
        if (_cache.Data.Shows.Count == 0)
        {
            _cache.Data.Shows = await _apiClient.GetShowsAsync();
            _cache.Save();
        }

        if (_queryOptions.Length == 0)
            return _cache.Data.Shows;

        if (!long.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid show ID '{_queryOptions[0]}'.");

        var show = _cache.Data.Shows.SingleOrDefault(a => a.Id == id);

        if (show == null)
        {
            show = await _apiClient.GetShowByIdAsync(id);
            if (show != null)
            {
                _cache.Data.Shows.Add(show);
                _cache.Save();
            }
        }

        return show;
    }

    private async Task<object> Songs()
    {
        if (_cache.Data.Songs.Count == 0)
        {
            _cache.Data.Songs = await _apiClient.GetSongsAsync();
            _cache.Save();
        }

        if (_queryOptions.Length == 0)
            return _cache.Data.Songs;

        if (!int.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid song ID '{_queryOptions[0]}'.");

        var song = _cache.Data.Songs.SingleOrDefault(a => a.Id == id);

        if (song == null)
        {
            song = await _apiClient.GetSongByIdAsync(id);
            if (song != null)
            {
                _cache.Data.Songs.Add(song);
                _cache.Save();
            }
        }

        return song;
    }

    private async Task<object> SongDatas()
    {
        if (_cache.Data.SongDatas.Count == 0)
        {
            _cache.Data.SongDatas = await _apiClient.GetSongDatasAsync();
            _cache.Save();
        }

        if (_queryOptions.Length == 0)
            return _cache.Data.SongDatas;

        if (!int.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid song ID '{_queryOptions[0]}'.");

        var songData = _cache.Data.SongDatas.SingleOrDefault(a => a.SongId == id);

        if (songData == null)
        {
            songData = await _apiClient.GetSongDataByIdAsync(id);
            if (songData != null)
            {
                _cache.Data.SongDatas.Add(songData);
                _cache.Save();
            }
        }

        return songData;
    }

    private async Task<object> Users()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        User user;

        switch (_queryOptions[0])
        {
            case "uid":
                if (!long.TryParse(_queryOptions[1], out var userId))
                    throw new PhishNetConsoleException($"Invalid show ID '{_queryOptions[1]}'.");
                user = _cache.Data.Users.SingleOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    user = await _apiClient.GetUserByIdAsync(userId);
                    _cache.Data.Users.Add(user);
                    _cache.Save();
                }
                break;
            case "username":
                var username = _queryOptions[1];
                user = _cache.Data.Users.SingleOrDefault(u => u.Username == username);
                if (user == null)
                {
                    user = await _apiClient.GetUserByUsernameAsync(_queryOptions[1]);
                    _cache.Data.Users.Add(user);
                    _cache.Save();
                }
                break; 
            default:
                throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.");
        }

        return user;
    }

    private async Task<object> Venues()
    {
        if (_cache.Data.Venues.Count == 0)
        {
            _cache.Data.Venues = await _apiClient.GetVenuesAsync();
            _cache.Save();
        }

        if (_queryOptions.Length == 0)
            return _cache.Data.Venues;

        if (!int.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid venue ID '{_queryOptions[0]}'.");

        var venue = _cache.Data.Venues.SingleOrDefault(a => a.Id == id);

        if (venue == null)
        {
            venue = await _apiClient.GetVenueByIdAsync(id);
            if (venue != null)
            {
                _cache.Data.Venues.Add(venue);
                _cache.Save();
            }
        }

        return venue;
    }

    #endregion

    private static void Log(string message)
    {
        System.Console.WriteLine(message);
    }
}
