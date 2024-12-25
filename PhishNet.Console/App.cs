using PhishNet.Models;
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
        var cacheKey = _queryOptions.Length == 0 ? "all" : _queryOptions[0];

        if (_cache.Artists.Data.TryGetValue(cacheKey, out var cachedArtists) && cachedArtists.Expiration > DateTime.Now)
        {
            return _queryOptions.Length == 0 ? cachedArtists.Items : cachedArtists.Items.SingleOrDefault();
        }

        if (_queryOptions.Length == 0)
        {
            var artists = await _apiClient.GetArtistsAsync();
            _cache.Artists.Data[cacheKey] = new CacheData<Artist>(artists);
            return artists;
        }

        if (!int.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid artist ID '{_queryOptions[0]}'.");
        var artist = await _apiClient.GetArtistByIdAsync(id);
        _cache.Artists.Data[cacheKey] = new CacheData<Artist>([artist]);
        return artist;
    }

    private async Task<object> Attendance()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        var cacheKey = $"{_queryOptions[0]}:{_queryOptions[1]}";

        if (_cache.Attendances.Data.TryGetValue(cacheKey, out var cachedAttendances) &&
            cachedAttendances.Expiration > DateTime.Now)
            return cachedAttendances.Items;

        var attendances = _queryOptions[0] switch
        {
            "uid" => await _apiClient.GetAttendanceByUserIdAsync(int.Parse(_queryOptions[1])),
            "username" => await _apiClient.GetAttendanceByUsernameAsync(_queryOptions[1]),
            "showid" => await _apiClient.GetAttendanceByShowIdAsync(long.Parse(_queryOptions[1])),
            "showdate" => await _apiClient.GetAttendanceByShowDateAsync(DateOnly.Parse(_queryOptions[1])),
            _ => throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.")
        };
        _cache.Attendances.Data[cacheKey] = new CacheData<Attendance>(attendances);
        return attendances;
    }

    private async Task<object> JamCharts()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        var cacheKey = $"{_queryOptions[0]}:{_queryOptions[1]}";

        if (_cache.JamCharts.Data.TryGetValue(cacheKey, out var cachedJamCharts) &&
            cachedJamCharts.Expiration > DateTime.Now)
            return cachedJamCharts.Items;

        var jamCharts = _queryOptions[0] switch
        {
            "slug" => await _apiClient.GetJamChartsBySongAsync(_queryOptions[1]),
            "showid" => await _apiClient.GetJamChartsByShowIdAsync(long.Parse(_queryOptions[1])),
            "showdate" => await _apiClient.GetJamChartsByShowDateAsync(DateOnly.Parse(_queryOptions[1])),
            _ => throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.")
        };
        _cache.JamCharts.Data[cacheKey] = new CacheData<JamChart>(jamCharts);
        return jamCharts;
    }

    private async Task<object> Reviews()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        var cacheKey = $"{_queryOptions[0]}:{_queryOptions[1]}";

        if (_cache.Reviews.Data.TryGetValue(cacheKey, out var cachedReviews) && cachedReviews.Expiration > DateTime.Now)
            return cachedReviews.Items;

        var reviews = _queryOptions[0] switch
        {
            "uid" => await _apiClient.GetReviewsByUserIdAsync(int.Parse(_queryOptions[1])),
            "username" => await _apiClient.GetReviewsByUsernameAsync(_queryOptions[1]),
            "showid" => await _apiClient.GetReviewsByShowIdAsync(long.Parse(_queryOptions[1])),
            "showdate" => await _apiClient.GetReviewsByShowDateAsync(DateOnly.Parse(_queryOptions[1])),
            _ => throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.")
        };
        _cache.Reviews.Data[cacheKey] = new CacheData<Review>(reviews);
        return reviews;
    }

    private async Task<object> Setlists()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        var cacheKey = $"{_queryOptions[0]}:{_queryOptions[1]}";

        if (_cache.Setlists.Data.TryGetValue(cacheKey, out var cachedSetlists) &&
            cachedSetlists.Expiration > DateTime.Now)
            return cachedSetlists.Items;

        var setlists = _queryOptions[0] switch
        {
            "slug" => await _apiClient.GetSetlistsBySongAsync(_queryOptions[1]),
            "showid" => await _apiClient.GetSetlistsByShowIdAsync(long.Parse(_queryOptions[1])),
            "showdate" => await _apiClient.GetSetlistsByShowDateAsync(DateOnly.Parse(_queryOptions[1])),
            _ => throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.")
        };
        _cache.Setlists.Data[cacheKey] = new CacheData<Setlist>(setlists);
        return setlists;
    }

    private async Task<object> Shows()
    {
        var cacheKey = _queryOptions.Length == 0 ? "all" : _queryOptions[0];

        if (_cache.Shows.Data.TryGetValue(cacheKey, out var cachedShows) && cachedShows.Expiration > DateTime.Now)
        {
            return _queryOptions.Length == 0 ? cachedShows.Items : cachedShows.Items.SingleOrDefault();
        }

        if (_queryOptions.Length == 0)
        {
            var shows = await _apiClient.GetShowsAsync();
            _cache.Shows.Data[cacheKey] = new CacheData<Show>(shows);
            return shows;
        }

        if (!long.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid show ID '{_queryOptions[0]}'.");
        var show = await _apiClient.GetShowByIdAsync(id);
        _cache.Shows.Data[cacheKey] = new CacheData<Show>([show]);
        return show;
    }

    private async Task<object> Songs()
    {
        var cacheKey = _queryOptions.Length == 0 ? "all" : _queryOptions[0];

        if (_cache.Songs.Data.TryGetValue(cacheKey, out var cachedSongs) && cachedSongs.Expiration > DateTime.Now)
        {
            return _queryOptions.Length == 0 ? cachedSongs.Items : cachedSongs.Items.SingleOrDefault();
        }

        if (_queryOptions.Length == 0)
        {
            var songs = await _apiClient.GetSongsAsync();
            _cache.Songs.Data[cacheKey] = new CacheData<Song>(songs);
            return songs;
        }

        if (!int.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid song ID '{_queryOptions[0]}'.");
        var song = await _apiClient.GetSongByIdAsync(id);
        _cache.Songs.Data[cacheKey] = new CacheData<Song>([song]);
        return song;
    }

    private async Task<object> SongDatas()
    {
        var cacheKey = _queryOptions.Length == 0 ? "all" : _queryOptions[0];

        if (_cache.SongDatas.Data.TryGetValue(cacheKey, out var cachedSongDatas) &&
            cachedSongDatas.Expiration > DateTime.Now)
        {
            return _queryOptions.Length == 0 ? cachedSongDatas.Items : cachedSongDatas.Items.SingleOrDefault();
        }

        if (_queryOptions.Length == 0)
        {
            var songDatas = await _apiClient.GetSongDatasAsync();
            _cache.SongDatas.Data[cacheKey] = new CacheData<SongData>(songDatas);
            return songDatas;
        }

        if (!int.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid song ID '{_queryOptions[0]}'.");
        var songData = await _apiClient.GetSongDataByIdAsync(id);
        _cache.SongDatas.Data[cacheKey] = new CacheData<SongData>([songData]);
        return songData;
    }

    private async Task<object> Users()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        var cacheKey = $"{_queryOptions[0]}:{_queryOptions[1]}";

        if (_cache.Users.Data.TryGetValue(cacheKey, out var cachedUsers) && cachedUsers.Expiration > DateTime.Now)
            return cachedUsers.Items.SingleOrDefault();

        var user = _queryOptions[0] switch
        {
            "uid" => await _apiClient.GetUserByIdAsync(long.Parse(_queryOptions[1])),
            "username" => await _apiClient.GetUserByUsernameAsync(_queryOptions[1]),
            _ => throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.")
        };
        _cache.Users.Data[cacheKey] = new CacheData<User>([user]);
        return user;
    }

    private async Task<object> Venues()
    {
        var cacheKey = _queryOptions.Length == 0 ? "all" : _queryOptions[0];

        if (_cache.Venues.Data.TryGetValue(cacheKey, out var cachedVenues) &&
            cachedVenues.Expiration > DateTime.Now)
        {
            return _queryOptions.Length == 0 ? cachedVenues.Items : cachedVenues.Items.SingleOrDefault();
        }

        if (_queryOptions.Length == 0)
        {
            var venues = await _apiClient.GetVenuesAsync();
            _cache.Venues.Data[cacheKey] = new CacheData<Venue>(venues);
            return venues;
        }

        if (!int.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid venue ID '{_queryOptions[0]}'.");
        var venue = await _apiClient.GetVenueByIdAsync(id);
        _cache.Venues.Data[cacheKey] = new CacheData<Venue>([venue]);
        return venue;
    }

    #endregion

    private static void Log(string message)
    {
        System.Console.WriteLine(message);
    }
}
