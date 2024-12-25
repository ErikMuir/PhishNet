using PhishNet.Models;
using PhishNet.Requests;
using System.Net;
using System.Text.Json;

namespace PhishNet;

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

    public Task<List<Artist>> GetArtistsAsync(PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new AllResourcesRequest(Resource.Artists, queryParams);
        return GetResourcesAsync<Artist>(request, ct);
    }

    public Task<Artist> GetArtistByIdAsync(int artistId, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new ResourceByIdRequest(Resource.Artists, artistId, queryParams);
        return GetResourceByIdAsync<Artist>(request, ct);
    }

    #endregion

    #region Attendance

    public Task<List<Attendance>> GetAttendanceByUserIdAsync(long userId, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Attendance, QueryableColumn.Uid, userId, queryParams);
        return QueryResourcesAsync<Attendance>(request, ct);
    }

    public Task<List<Attendance>> GetAttendanceByUsernameAsync(string username, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Attendance, QueryableColumn.Username, username, queryParams);
        return QueryResourcesAsync<Attendance>(request, ct);
    }

    public Task<List<Attendance>> GetAttendanceByShowIdAsync(long showId, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Attendance, QueryableColumn.ShowId, showId, queryParams);
        return QueryResourcesAsync<Attendance>(request, ct);
    }

    public Task<List<Attendance>> GetAttendanceByShowDateAsync(DateOnly showDate, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Attendance, QueryableColumn.ShowDate, showDate, queryParams);
        return QueryResourcesAsync<Attendance>(request, ct);
    }

    #endregion

    #region JamCharts

    public Task<List<JamChart>> GetJamChartsBySongAsync(string songSlug, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.JamCharts, QueryableColumn.Slug, songSlug, queryParams);
        return QueryResourcesAsync<JamChart>(request, ct);
    }

    public Task<List<JamChart>> GetJamChartsByShowIdAsync(long showId, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.JamCharts, QueryableColumn.ShowId, showId, queryParams);
        return QueryResourcesAsync<JamChart>(request, ct);
    }

    public Task<List<JamChart>> GetJamChartsByShowDateAsync(DateOnly showDate, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.JamCharts, QueryableColumn.ShowDate, showDate, queryParams);
        return QueryResourcesAsync<JamChart>(request, ct);
    }

    #endregion

    #region Reviews

    public Task<List<Review>> GetReviewsByUserIdAsync(long userId, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Reviews, QueryableColumn.Uid, userId, queryParams);
        return QueryResourcesAsync<Review>(request, ct);
    }

    public Task<List<Review>> GetReviewsByUsernameAsync(string username, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Reviews, QueryableColumn.Username, username, queryParams);
        return QueryResourcesAsync<Review>(request, ct);
    }

    public Task<List<Review>> GetReviewsByShowIdAsync(long showId, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Reviews, QueryableColumn.ShowId, showId, queryParams);
        return QueryResourcesAsync<Review>(request, ct);
    }

    public Task<List<Review>> GetReviewsByShowDateAsync(DateOnly showDate, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Reviews, QueryableColumn.ShowDate, showDate, queryParams);
        return QueryResourcesAsync<Review>(request, ct);
    }

    #endregion

    #region Setlists

    public Task<List<Setlist>> GetSetlistsBySongAsync(string songSlug, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Setlists, QueryableColumn.Slug, songSlug, queryParams);
        return QueryResourcesAsync<Setlist>(request, ct);
    }

    public Task<List<Setlist>> GetSetlistsByShowIdAsync(long showId, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Setlists, QueryableColumn.ShowId, showId, queryParams);
        return QueryResourcesAsync<Setlist>(request, ct);
    }

    public Task<List<Setlist>> GetSetlistsByShowDateAsync(DateOnly showDate, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Setlists, QueryableColumn.ShowDate, showDate, queryParams);
        return QueryResourcesAsync<Setlist>(request, ct);
    }

    #endregion

    #region Shows

    public Task<List<Show>> GetShowsAsync(PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new AllResourcesRequest(Resource.Shows, queryParams);
        return GetResourcesAsync<Show>(request, ct);
    }

    public Task<Show> GetShowByIdAsync(long showId, CancellationToken ct = new())
    {
        var request = new ResourceByIdRequest(Resource.Shows, showId);
        return GetResourceByIdAsync<Show>(request, ct);
    }

    #endregion

    #region Songs

    public Task<List<Song>> GetSongsAsync(PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new AllResourcesRequest(Resource.Songs, queryParams);
        return GetResourcesAsync<Song>(request, ct);
    }

    public Task<Song> GetSongByIdAsync(int songId, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new ResourceByIdRequest(Resource.Songs, songId, queryParams);
        return GetResourceByIdAsync<Song>(request, ct);
    }

    #endregion

    #region SongDatas

    public Task<List<SongData>> GetSongDatasAsync(PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new AllResourcesRequest(Resource.SongData, queryParams);
        return GetResourcesAsync<SongData>(request, ct);
    }

    public Task<SongData> GetSongDataByIdAsync(int songId, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new ResourceByIdRequest(Resource.SongData, songId, queryParams);
        return GetResourceByIdAsync<SongData>(request, ct);
    }

    #endregion

    #region Users

    public async Task<User> GetUserByIdAsync(long userId, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Users, QueryableColumn.Uid, userId, queryParams);
        return (await QueryResourcesAsync<User>(request, ct)).FirstOrDefault();
    }

    public async Task<User> GetUserByUsernameAsync(string username, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new QueryResourcesRequest(Resource.Users, QueryableColumn.Username, username, queryParams);
        return (await QueryResourcesAsync<User>(request, ct)).FirstOrDefault();
    }

    #endregion

    #region Venues

    public Task<List<Venue>> GetVenuesAsync(PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new AllResourcesRequest(Resource.Venues, queryParams);
        return GetResourcesAsync<Venue>(request, ct);
    }

    public Task<Venue> GetVenueByIdAsync(int venueId, PhishNetApiQueryParams queryParams = null, CancellationToken ct = new())
    {
        var request = new ResourceByIdRequest(Resource.Venues, venueId, queryParams);
        return GetResourceByIdAsync<Venue>(request, ct);
    }

    #endregion

    public Task<List<T>> GetResourcesAsync<T>(
        AllResourcesRequest request,
        CancellationToken ct = new())
        where T : IPhishNetApiResource
        => GetListAsync<T>(request, ct);

    public async Task<T> GetResourceByIdAsync<T>(
        ResourceByIdRequest request,
        CancellationToken ct = new())
        where T : IPhishNetApiResource
        => (await GetListAsync<T>(request, ct)).FirstOrDefault();

    public Task<List<T>> QueryResourcesAsync<T>(
        QueryResourcesRequest request,
        CancellationToken ct = new())
        where T : IPhishNetApiResource
        => GetListAsync<T>(request, ct);

    private async Task<List<T>> GetListAsync<T>(
        ResourceRequest resourceRequest,
        CancellationToken ct = new())
        where T : IPhishNetApiResource
    {
        try
        {
            var httpRequest = new HttpRequestMessage { Method = HttpMethod.Get, RequestUri = resourceRequest.GetRequestUri(_config) };

            var response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseContentRead, ct);
            switch (response.StatusCode)
            {
                case < HttpStatusCode.BadRequest:
                    break;
                case HttpStatusCode.NotFound:
                    return [];
                default:
                    throw new PhishNetApiException(response.ReasonPhrase);
            }

            var content = await response.Content.ReadAsStringAsync(ct);
            var dataResponse = JsonSerializer.Deserialize<DataResponse<T>>(content);
            if (dataResponse is null) throw new PhishNetApiException("Failed to deserialize response.");

            if (dataResponse.IsError) throw new PhishNetApiException($"{dataResponse.ErrorMessage} (ErrorCode: {dataResponse.ErrorCode})");

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
}
