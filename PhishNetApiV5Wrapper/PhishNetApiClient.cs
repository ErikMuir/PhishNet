using Microsoft.AspNetCore.WebUtilities;
using PhishNetApiV5Wrapper.Models;
using PhishNetApiV5Wrapper.Requests;
using System.Text.Json;

namespace PhishNetApiV5Wrapper;

public class PhishNetApiClient
{
    private readonly PhishNetApiClientConfig _config;
    private readonly HttpClient _httpClient;

    public PhishNetApiClient() : this(new PhishNetApiClientConfig()) { }

    public PhishNetApiClient(PhishNetApiClientConfig config)
    {
        config ??= new PhishNetApiClientConfig();
        if (string.IsNullOrWhiteSpace(config.ApiKey))
            throw new PhishNetApiException("Api key is required.");

        _config = config;
        _httpClient = new HttpClient { BaseAddress = new Uri(_config.BaseUrl) };
    }


    #region Artists

    public Task<List<Artist>> GetArtistsAsync(CancellationToken ct = new())
    {
        var request = new GetResourcesRequest(Resource.Artists);
        return GetResourcesAsync<Artist>(request, new Dictionary<string, string>(), ct);
    }

    public Task<Artist> GetArtistByIdAsync(int artistId, CancellationToken ct = new())
    {
        var request = new GetResourceByIdRequest(Resource.Artists, artistId);
        return GetResourceByIdAsync<Artist>(request, new Dictionary<string, string>(), ct);
    }

    #endregion

    #region Attendance

    public Task<List<Attendance>> GetAttendanceByUserIdAsync(long userId, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Attendance, QueryableColumn.Uid, userId);
        return QueryResourcesAsync<Attendance>(request, new Dictionary<string, string>(), ct);
    }

    public Task<List<Attendance>> GetAttendanceByUsernameAsync(string username, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Attendance, QueryableColumn.Username, username);
        return QueryResourcesAsync<Attendance>(request, new Dictionary<string, string>(), ct);
    }

    public Task<List<Attendance>> GetAttendanceByShowIdAsync(long showId, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Attendance, QueryableColumn.ShowId, showId);
        return QueryResourcesAsync<Attendance>(request, new Dictionary<string, string>(), ct);
    }

    public Task<List<Attendance>> GetAttendanceByShowDateAsync(DateOnly showDate, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Attendance, QueryableColumn.ShowDate, showDate);
        return QueryResourcesAsync<Attendance>(request, new Dictionary<string, string>(), ct);
    }

    #endregion

    #region JamCharts

    public Task<List<JamChart>> GetJamChartsBySongAsync(string songSlug, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.JamCharts, QueryableColumn.Slug, songSlug);
        return QueryResourcesAsync<JamChart>(request, new Dictionary<string, string>(), ct);
    }

    public Task<List<JamChart>> GetJamChartsByShowIdAsync(long showId, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.JamCharts, QueryableColumn.ShowId, showId);
        return QueryResourcesAsync<JamChart>(request, new Dictionary<string, string>(), ct);
    }

    public Task<List<JamChart>> GetJamChartsByShowDateAsync(DateOnly showDate, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.JamCharts, QueryableColumn.ShowDate, showDate);
        return QueryResourcesAsync<JamChart>(request, new Dictionary<string, string>(), ct);
    }

    #endregion

    #region Reviews

    public Task<List<Review>> GetReviewsByUserIdAsync(long userId, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Reviews, QueryableColumn.Uid, userId);
        return QueryResourcesAsync<Review>(request, new Dictionary<string, string>(), ct);
    }

    public Task<List<Review>> GetReviewsByUsernameAsync(string username, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Reviews, QueryableColumn.Username, username);
        return QueryResourcesAsync<Review>(request, new Dictionary<string, string>(), ct);
    }

    public Task<List<Review>> GetReviewsByShowIdAsync(long showId, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Reviews, QueryableColumn.ShowId, showId);
        return QueryResourcesAsync<Review>(request, new Dictionary<string, string>(), ct);
    }

    public Task<List<Review>> GetReviewsByShowDateAsync(DateOnly showDate, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Reviews, QueryableColumn.ShowDate, showDate);
        return QueryResourcesAsync<Review>(request, new Dictionary<string, string>(), ct);
    }

    #endregion

    #region Setlists

    public async Task<Setlist> GetSetlistByShowIdAsync(long showId, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Setlists, QueryableColumn.ShowId, showId);
        return (Setlist) await QueryResourcesAsync<SongPerformance>(request, new Dictionary<string, string>(), ct);
    }

    public async Task<Setlist> GetSetlistByShowDateAsync(DateOnly showDate, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Setlists, QueryableColumn.ShowDate, showDate);
        return (Setlist) await QueryResourcesAsync<SongPerformance>(request, new Dictionary<string, string>(), ct);
    }

    #endregion

    #region Shows

    public Task<List<Show>> GetShowsAsync(CancellationToken ct = new())
    {
        var request = new GetResourcesRequest(Resource.Shows);
        return GetResourcesAsync<Show>(request, new Dictionary<string, string>(), ct);
    }

    public Task<Show> GetShowByIdAsync(long showId, CancellationToken ct = new())
    {
        var request = new GetResourceByIdRequest(Resource.Shows, showId);
        return GetResourceByIdAsync<Show>(request, new Dictionary<string, string>(), ct);
    }

    #endregion

    #region Songs

    public Task<List<Song>> GetSongsAsync(CancellationToken ct = new())
    {
        var request = new GetResourcesRequest(Resource.Songs);
        return GetResourcesAsync<Song>(request, new Dictionary<string, string>(), ct);
    }

    public Task<Song> GetSongByIdAsync(int songId, CancellationToken ct = new())
    {
        var request = new GetResourceByIdRequest(Resource.Songs, songId);
        return GetResourceByIdAsync<Song>(request, new Dictionary<string, string>(), ct);
    }

    #endregion

    #region SongData

    public Task<List<SongData>> GetSongDataAsync(CancellationToken ct = new())
    {
        var request = new GetResourcesRequest(Resource.SongData);
        return GetResourcesAsync<SongData>(request, new Dictionary<string, string>(), ct);
    }

    public Task<SongData> GetSongDataByIdAsync(int songId, CancellationToken ct = new())
    {
        var request = new GetResourceByIdRequest(Resource.SongData, songId);
        return GetResourceByIdAsync<SongData>(request, new Dictionary<string, string>(), ct);
    }

    #endregion

    #region SongPerformances

    public Task<List<SongPerformance>> GetSongPerformancesAsync(string songSlug, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Setlists, QueryableColumn.Slug, songSlug);
        return QueryResourcesAsync<SongPerformance>(request, new Dictionary<string, string>(), ct);
    }

    #endregion

    #region Users

    public async Task<User> GetUserByIdAsync(long userId, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Users, QueryableColumn.Uid, userId);
        return (await QueryResourcesAsync<User>(request, new Dictionary<string, string>(), ct)).FirstOrDefault();
    }

    public async Task<User> GetUserByUsernameAsync(string username, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Users, QueryableColumn.Username, username);
        return (await QueryResourcesAsync<User>(request, new Dictionary<string, string>(), ct)).FirstOrDefault();
    }

    #endregion

    #region Venues

    public Task<List<Venue>> GetVenuesAsync(CancellationToken ct = new())
    {
        var request = new GetResourcesRequest(Resource.Venues);
        return GetResourcesAsync<Venue>(request, new Dictionary<string, string>(), ct);
    }

    public Task<Venue> GetVenueByIdAsync(int id, CancellationToken ct = new())
    {
        var request = new GetResourceByIdRequest(Resource.Venues, id);
        return GetResourceByIdAsync<Venue>(request, new Dictionary<string, string>(), ct);
    }

    #endregion

    public Task<List<T>> GetResourcesAsync<T>(
        GetResourcesRequest request,
        Dictionary<string, string> queryParams,
        CancellationToken ct = new())
        where T : IPhishNetApiResource
        => GetListAsync<T>(request.Path, queryParams, ct);

    public async Task<T> GetResourceByIdAsync<T>(
        GetResourceByIdRequest request,
        Dictionary<string, string> queryParams,
        CancellationToken ct = new())
        where T : IPhishNetApiResource
        => (await GetListAsync<T>(request.Path, queryParams, ct)).FirstOrDefault();

    public Task<List<T>> QueryResourcesAsync<T>(
        QueryResourcesRequest resourcesRequest,
        Dictionary<string, string> queryParams,
        CancellationToken ct = new())
        where T : IPhishNetApiResource
        => GetListAsync<T>(resourcesRequest.Path, queryParams, ct);

    private async Task<List<T>> GetListAsync<T>(
        string url,
        Dictionary<string, string> queryParams,
        CancellationToken ct = new())
        where T : IPhishNetApiResource
    {
        try
        {
            url = $"{_config.BaseUrl}/{url}.{_config.Format}";
            Log($"GET {url}");

            queryParams ??= new Dictionary<string, string>();
            queryParams.TryAdd("apikey", _config.ApiKey);

            var requestUri = new Uri(QueryHelpers.AddQueryString(url, queryParams));
            var request = new HttpRequestMessage { Method = HttpMethod.Get, RequestUri = requestUri };

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct);
            if (!response.IsSuccessStatusCode) throw new PhishNetApiException(response.ReasonPhrase);

            var content = await response.Content.ReadAsStringAsync(ct);
            var dataResponse = JsonSerializer.Deserialize<DataResponse<T>>(content);
            if (dataResponse is null) throw new PhishNetApiException("Failed to deserialize response.");

            if (dataResponse.IsError) throw new PhishNetApiException($"{dataResponse.ErrorMessage} (ErrorCode: {dataResponse.ErrorCode})");

            Log($"Retrieved {dataResponse.Data.Count} record(s).");

            return dataResponse.Data;
        }
        catch (PhishNetApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new PhishNetApiException("Unexpected exception occurred.", ex);
        }
    }

    private void Log(string message)
    {
        if (_config.LogsEnabled)
            Console.WriteLine(message);
    }
}
