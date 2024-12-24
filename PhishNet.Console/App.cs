using System.Text.Json;

namespace PhishNet.Console;

public class App
{
    private readonly string _resource;
    private readonly string[] _queryOptions;
    private readonly bool _showHelp;
    private readonly PhishNetApiClient _apiClient;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly Cache _cache;
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
        _cache = new Cache();

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
        var isAll = _queryOptions.Length == 0;
        var cacheKey = isAll ? "all" : _queryOptions[0];

        if (_cache.Artists.Data.TryGetValue(cacheKey, out var artists))
            return isAll ? artists : artists.SingleOrDefault();

        if (isAll)
        {
            artists = await _apiClient.GetArtistsAsync();
            _cache.Artists.Data.Add(cacheKey, artists);
            return artists;
        }

        if (!int.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid artist ID '{_queryOptions[0]}'.");
        var artist = await _apiClient.GetArtistByIdAsync(id);
        _cache.Artists.Data.Add(cacheKey, [artist]);
        return artist;
    }

    private async Task<object> Attendance()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        var cacheKey = $"{_queryOptions[0]}:{_queryOptions[1]}";

        if (_cache.Attendances.Data.TryGetValue(cacheKey, out var attendances))
            return attendances;

        attendances = _queryOptions[0] switch
        {
            "uid" => await _apiClient.GetAttendanceByUserIdAsync(int.Parse(_queryOptions[1])),
            "username" => await _apiClient.GetAttendanceByUsernameAsync(_queryOptions[1]),
            "showid" => await _apiClient.GetAttendanceByShowIdAsync(long.Parse(_queryOptions[1])),
            "showdate" => await _apiClient.GetAttendanceByShowDateAsync(DateOnly.Parse(_queryOptions[1])),
            _ => throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.")
        };

        _cache.Attendances.Data.Add(cacheKey, attendances);

        return attendances;
    }

    private async Task<object> JamCharts()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        var cacheKey = $"{_queryOptions[0]}:{_queryOptions[1]}";

        if (_cache.JamCharts.Data.TryGetValue(cacheKey, out var jamCharts))
            return jamCharts;

        jamCharts = _queryOptions[0] switch
        {
            "slug" => await _apiClient.GetJamChartsBySongAsync(_queryOptions[1]),
            "showid" => await _apiClient.GetJamChartsByShowIdAsync(long.Parse(_queryOptions[1])),
            "showdate" => await _apiClient.GetJamChartsByShowDateAsync(DateOnly.Parse(_queryOptions[1])),
            _ => throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.")
        };

        _cache.JamCharts.Data.Add(cacheKey, jamCharts);

        return jamCharts;
    }

    private async Task<object> Reviews()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        var cacheKey = $"{_queryOptions[0]}:{_queryOptions[1]}";

        if (_cache.Reviews.Data.TryGetValue(cacheKey, out var reviews))
            return reviews;

        reviews = _queryOptions[0] switch
        {
            "uid" => await _apiClient.GetReviewsByUserIdAsync(int.Parse(_queryOptions[1])),
            "username" => await _apiClient.GetReviewsByUsernameAsync(_queryOptions[1]),
            "showid" => await _apiClient.GetReviewsByShowIdAsync(long.Parse(_queryOptions[1])),
            "showdate" => await _apiClient.GetReviewsByShowDateAsync(DateOnly.Parse(_queryOptions[1])),
            _ => throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.")
        };

        _cache.Reviews.Data.Add(cacheKey, reviews);

        return reviews;
    }

    private async Task<object> Setlists()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        var cacheKey = $"{_queryOptions[0]}:{_queryOptions[1]}";

        if (_cache.SetlistItems.Data.TryGetValue(cacheKey, out var setlistItems))
            return setlistItems;

        setlistItems = _queryOptions[0] switch
        {
            "slug" => await _apiClient.GetSongPerformancesAsync(_queryOptions[1]),
            "showid" => await _apiClient.GetSetlistByShowIdAsync(long.Parse(_queryOptions[1])),
            "showdate" => await _apiClient.GetSetlistByShowDateAsync(DateOnly.Parse(_queryOptions[1])),
            _ => throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.")
        };

        _cache.SetlistItems.Data.Add(cacheKey, setlistItems);

        return setlistItems;
    }

    private async Task<object> Shows()
    {
        var isAll = _queryOptions.Length == 0;
        var cacheKey = isAll ? "all" : _queryOptions[0];

        if (_cache.Shows.Data.TryGetValue(cacheKey, out var shows))
            return isAll ? shows : shows.SingleOrDefault();

        if (isAll)
        {
            shows = await _apiClient.GetShowsAsync();
            _cache.Shows.Data.Add(cacheKey, shows);
            return shows;
        }

        if (!long.TryParse(_queryOptions[1], out var id))
            throw new PhishNetConsoleException($"Invalid show ID '{_queryOptions[1]}'.");
        var show = await _apiClient.GetShowByIdAsync(id);
        _cache.Shows.Data.Add(cacheKey, [show]);
        return show;
    }

    private async Task<object> Songs()
    {
        var isAll = _queryOptions.Length == 0;
        var cacheKey = isAll ? "all" : _queryOptions[0];

        if (_cache.Songs.Data.TryGetValue(cacheKey, out var songs))
            return isAll ? songs : songs.SingleOrDefault();

        if (isAll)
        {
            songs = await _apiClient.GetSongsAsync();
            _cache.Songs.Data.Add(cacheKey, songs);
            return songs;
        }

        if (!int.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid song ID '{_queryOptions[0]}'.");
        var song = await _apiClient.GetSongByIdAsync(id);
        _cache.Songs.Data.Add(cacheKey, [song]);
        return song;
    }

    private async Task<object> SongDatas()
    {
        var isAll = _queryOptions.Length == 0;
        var cacheKey = isAll ? "all" : _queryOptions[0];

        if (_cache.SongDatas.Data.TryGetValue(cacheKey, out var songDatas))
            return isAll ? songDatas : songDatas.SingleOrDefault();

        if (isAll)
        {
            songDatas = await _apiClient.GetSongDatasAsync();
            _cache.SongDatas.Data.Add(cacheKey, songDatas);
            return songDatas;
        }

        if (!int.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid song ID '{_queryOptions[0]}'.");
        var songData = await _apiClient.GetSongDataByIdAsync(id);
        _cache.SongDatas.Data.Add(cacheKey, [songData]);
        return songData;
    }

    private async Task<object> Users()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        var cacheKey = $"{_queryOptions[0]}:{_queryOptions[1]}";

        if (_cache.Users.Data.TryGetValue(cacheKey, out var users))
            return users.SingleOrDefault();

        var user = _queryOptions[0] switch
        {
            "uid" => await _apiClient.GetUserByIdAsync(long.Parse(_queryOptions[1])),
            "username" => await _apiClient.GetUserByUsernameAsync(_queryOptions[1]),
            _ => throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.")
        };

        _cache.Users.Data.Add(cacheKey, [user]);

        return user;
    }

    private async Task<object> Venues()
    {
        var isAll = _queryOptions.Length == 0;
        var cacheKey = isAll ? "all" : _queryOptions[0];

        if (_cache.Venues.Data.TryGetValue(cacheKey, out var venues))
            return isAll ? venues : venues.SingleOrDefault();

        if (isAll)
        {
            venues = await _apiClient.GetVenuesAsync();
            _cache.Venues.Data.Add(cacheKey, venues);
            return venues;
        }

        if (!int.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid venue ID '{_queryOptions[0]}'.");
        var venue = await _apiClient.GetVenueByIdAsync(id);
        _cache.Venues.Data.Add(cacheKey, [venue]);
        return venue;
    }

    #endregion

    private static void Log(string message)
    {
        System.Console.WriteLine(message);
    }
}
