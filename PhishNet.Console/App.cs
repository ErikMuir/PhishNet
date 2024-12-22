using System.Text.Json;

namespace PhishNet.Console;

public class App
{
    private readonly string _resource;
    private readonly string[] _queryOptions;
    private readonly bool _showHelp;
    private readonly PhishNetApiClient _apiClient;
    private readonly JsonSerializerOptions _serializerOptions;
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
                "songdata" => await SongsData(),
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
    }

    #region Resource Actions

    private async Task<object> Artists()
    {
        if (_queryOptions.Length == 0)
            return await _apiClient.GetArtistsAsync();

        if (!int.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid artist ID '{_queryOptions[0]}'.");

        return await _apiClient.GetArtistByIdAsync(id);
    }

    private async Task<object> Attendance()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        switch (_queryOptions[0])
        {
            case "uid":
                if (!int.TryParse(_queryOptions[1], out var userId))
                    throw new PhishNetConsoleException($"Invalid user ID '{_queryOptions[1]}'.");
                return await _apiClient.GetAttendanceByUserIdAsync(userId);
            case "username":
                return await _apiClient.GetAttendanceByUsernameAsync(_queryOptions[1]);
            case "showid":
                if (!long.TryParse(_queryOptions[1], out var showId))
                    throw new PhishNetConsoleException($"Invalid show ID '{_queryOptions[1]}'.");
                return await _apiClient.GetAttendanceByShowIdAsync(showId);
            case "showdate":
                if (!DateOnly.TryParse(_queryOptions[1], out var showDate))
                    throw new PhishNetConsoleException($"Invalid show date '{_queryOptions[1]}'.");
                return await _apiClient.GetAttendanceByShowDateAsync(showDate);
            default:
                throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.");
        }
    }

    private async Task<object> JamCharts()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        switch (_queryOptions[0])
        {
            case "slug":
                return await _apiClient.GetJamChartsBySongAsync(_queryOptions[1]);
            case "showid":
                if (!long.TryParse(_queryOptions[1], out var showId))
                    throw new PhishNetConsoleException($"Invalid show ID '{_queryOptions[1]}'.");
                return await _apiClient.GetJamChartsByShowIdAsync(showId);
            case "showdate":
                if (!DateOnly.TryParse(_queryOptions[1], out var showDate))
                    throw new PhishNetConsoleException($"Invalid show date '{_queryOptions[1]}'.");
                return await _apiClient.GetJamChartsByShowDateAsync(showDate);
            default:
                throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.");
        }
    }

    private async Task<object> Reviews()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        switch (_queryOptions[0])
        {
            case "uid":
                if (!int.TryParse(_queryOptions[1], out var userId))
                    throw new PhishNetConsoleException($"Invalid user ID '{_queryOptions[1]}'.");
                return await _apiClient.GetReviewsByUserIdAsync(userId);
            case "username":
                return await _apiClient.GetReviewsByUsernameAsync(_queryOptions[1]);
            case "showid":
                if (!long.TryParse(_queryOptions[1], out var showId))
                    throw new PhishNetConsoleException($"Invalid show ID '{_queryOptions[1]}'.");
                return await _apiClient.GetReviewsByShowIdAsync(showId);
            case "showdate":
                if (!DateOnly.TryParse(_queryOptions[1], out var showDate))
                    throw new PhishNetConsoleException($"Invalid show date '{_queryOptions[1]}'.");
                return await _apiClient.GetReviewsByShowDateAsync(showDate);
            default:
                throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.");
        }
    }

    private async Task<object> Setlists()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        switch (_queryOptions[0])
        {
            case "slug":
                return await _apiClient.GetSongPerformancesAsync(_queryOptions[1]);
            case "showid":
                if (!long.TryParse(_queryOptions[1], out var showId))
                    throw new PhishNetConsoleException($"Invalid show ID '{_queryOptions[1]}'.");
                return await _apiClient.GetSetlistByShowIdAsync(showId);
            case "showdate":
                if (!DateOnly.TryParse(_queryOptions[1], out var showDate))
                    throw new PhishNetConsoleException($"Invalid show date '{_queryOptions[1]}'.");
                return await _apiClient.GetSetlistByShowDateAsync(showDate);
            default:
                throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.");
        }
    }

    private async Task<object> Shows()
    {
        if (_queryOptions.Length == 0)
            return await _apiClient.GetShowsAsync();

        if (!long.TryParse(_queryOptions[0], out var showId))
            throw new PhishNetConsoleException($"Invalid show ID '{_queryOptions[0]}'.");

        return await _apiClient.GetShowByIdAsync(showId);
    }

    private async Task<object> Songs()
    {
        if (_queryOptions.Length == 0)
            return await _apiClient.GetSongsAsync();

        if (!int.TryParse(_queryOptions[0], out var songId))
            throw new PhishNetConsoleException($"Invalid song ID '{_queryOptions[0]}'.");

        return await _apiClient.GetSongByIdAsync(songId);
    }

    private async Task<object> SongsData()
    {
        if (_queryOptions.Length == 0)
            return await _apiClient.GetSongDataAsync();

        if (!int.TryParse(_queryOptions[0], out var songId))
            throw new PhishNetConsoleException($"Invalid song ID '{_queryOptions[0]}'.");

        return await _apiClient.GetSongDataByIdAsync(songId);
    }

    private async Task<object> Users()
    {
        if (_queryOptions.Length < 1)
            throw new PhishNetConsoleException("Missing required query column.");
        if (_queryOptions.Length < 2)
            throw new PhishNetConsoleException("Missing required query value.");

        switch (_queryOptions[0])
        {
            case "uid":
                if (!long.TryParse(_queryOptions[1], out var userId))
                    throw new PhishNetConsoleException($"Invalid show ID '{_queryOptions[1]}'.");
                return await _apiClient.GetUserByIdAsync(userId);
            case "username":
                return await _apiClient.GetUserByUsernameAsync(_queryOptions[1]);
            default:
                throw new PhishNetConsoleException($"Invalid query column '{_queryOptions[0]}'.");
        }
    }

    private async Task<object> Venues()
    {
        if (_queryOptions.Length == 0)
            return await _apiClient.GetVenuesAsync();

        if (!int.TryParse(_queryOptions[0], out var id))
            throw new PhishNetConsoleException($"Invalid venue ID '{_queryOptions[0]}'.");

        return await _apiClient.GetVenueByIdAsync(id);
    }

    #endregion

    private static void Log(string message)
    {
        System.Console.WriteLine(message);
    }
}