using PhishNet.Models;
using PhishNet.Requests;
using System.Net;
using System.Text.Json;

namespace PhishNet;

/// <summary>
/// Provides methods to interact with the Phish.net API.
/// </summary>
public class PhishNetApiClient : IPhishNetApiClient
{
    private readonly PhishNetApiClientConfig _config;
    private readonly HttpClient _httpClient;

    public PhishNetApiClient() : this(new PhishNetApiClientConfig()) { }

    public PhishNetApiClient(PhishNetApiClientConfig config)
    {
        config ??= new PhishNetApiClientConfig();
        if (string.IsNullOrWhiteSpace(config.ApiKey))
            throw new PhishNetApiException("API key is required. Can be provided using the PHISH_NET_API_KEY environment variable.");

        _config = config;
        _httpClient = new HttpClient { BaseAddress = new Uri(_config.BaseUrl) };
    }


    #region Artists

    /// <summary>
    /// Retrieve all artists.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Artist>> GetArtistsAsync(PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new AllResourcesRequest(Resource.Artists, queryParams);
        return GetResourcesAsync<Artist>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve an artist by id.
    /// </summary>
    /// <param name="artistId"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Artist> GetArtistByIdAsync(int artistId, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new ResourceByIdRequest(Resource.Artists, artistId, queryParams);
        return GetResourceByIdAsync<Artist>(request, cancellationToken);
    }

    #endregion

    #region Attendance

    /// <summary>
    /// Retrieve attendance by user id.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Attendance>> GetAttendanceByUserIdAsync(long userId, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.Attendance, QueryableColumn.Uid, userId, queryParams);
        return QueryResourcesAsync<Attendance>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve attendance by username.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Attendance>> GetAttendanceByUsernameAsync(string username, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.Attendance, QueryableColumn.Username, username, queryParams);
        return QueryResourcesAsync<Attendance>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve attendance by show id.
    /// </summary>
    /// <param name="showId"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Attendance>> GetAttendanceByShowIdAsync(long showId, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.Attendance, QueryableColumn.ShowId, showId, queryParams);
        return QueryResourcesAsync<Attendance>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve attendance by show date.
    /// </summary>
    /// <param name="showDate"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Attendance>> GetAttendanceByShowDateAsync(DateOnly showDate, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.Attendance, QueryableColumn.ShowDate, showDate, queryParams);
        return QueryResourcesAsync<Attendance>(request, cancellationToken);
    }

    #endregion

    #region JamCharts

    /// <summary>
    /// Retrieve jam charts by song slug.
    /// </summary>
    /// <param name="songSlug"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<JamChart>> GetJamChartsBySongAsync(string songSlug, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.JamCharts, QueryableColumn.Slug, songSlug, queryParams);
        return QueryResourcesAsync<JamChart>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve jam charts by show id.
    /// </summary>
    /// <param name="showId"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<JamChart>> GetJamChartsByShowIdAsync(long showId, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.JamCharts, QueryableColumn.ShowId, showId, queryParams);
        return QueryResourcesAsync<JamChart>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve jam charts by show date.
    /// </summary>
    /// <param name="showDate"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<JamChart>> GetJamChartsByShowDateAsync(DateOnly showDate, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.JamCharts, QueryableColumn.ShowDate, showDate, queryParams);
        return QueryResourcesAsync<JamChart>(request, cancellationToken);
    }

    #endregion

    #region Reviews

    /// <summary>
    /// Retrieve reviews by user id.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Review>> GetReviewsByUserIdAsync(long userId, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.Reviews, QueryableColumn.Uid, userId, queryParams);
        return QueryResourcesAsync<Review>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve reviews by username.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Review>> GetReviewsByUsernameAsync(string username, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.Reviews, QueryableColumn.Username, username, queryParams);
        return QueryResourcesAsync<Review>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve reviews by show id.
    /// </summary>
    /// <param name="showId"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Review>> GetReviewsByShowIdAsync(long showId, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.Reviews, QueryableColumn.ShowId, showId, queryParams);
        return QueryResourcesAsync<Review>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve reviews by show date.
    /// </summary>
    /// <param name="showDate"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Review>> GetReviewsByShowDateAsync(DateOnly showDate, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.Reviews, QueryableColumn.ShowDate, showDate, queryParams);
        return QueryResourcesAsync<Review>(request, cancellationToken);
    }

    #endregion

    #region Setlists

    /// <summary>
    /// Retrieve setlists by song slug.
    /// </summary>
    /// <param name="songSlug"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Setlist>> GetSetlistsBySongAsync(string songSlug, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.Setlists, QueryableColumn.Slug, songSlug, queryParams);
        return QueryResourcesAsync<Setlist>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve setlists by show id.
    /// </summary>
    /// <param name="showId"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Setlist>> GetSetlistsByShowIdAsync(long showId, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.Setlists, QueryableColumn.ShowId, showId, queryParams);
        return QueryResourcesAsync<Setlist>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve setlists by show date.
    /// </summary>
    /// <param name="showDate"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Setlist>> GetSetlistsByShowDateAsync(DateOnly showDate, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.Setlists, QueryableColumn.ShowDate, showDate, queryParams);
        return QueryResourcesAsync<Setlist>(request, cancellationToken);
    }

    #endregion

    #region Shows

    /// <summary>
    /// Retrieve all shows.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Show>> GetShowsAsync(PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new AllResourcesRequest(Resource.Shows, queryParams);
        return GetResourcesAsync<Show>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve a show by id.
    /// </summary>
    /// <param name="showId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Show> GetShowByIdAsync(long showId, CancellationToken cancellationToken = new())
    {
        var request = new ResourceByIdRequest(Resource.Shows, showId);
        return GetResourceByIdAsync<Show>(request, cancellationToken);
    }

    #endregion

    #region Songs

    /// <summary>
    /// Retrieve all songs.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Song>> GetSongsAsync(PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new AllResourcesRequest(Resource.Songs, queryParams);
        return GetResourcesAsync<Song>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve a song by id.
    /// </summary>
    /// <param name="songId"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Song> GetSongByIdAsync(int songId, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new ResourceByIdRequest(Resource.Songs, songId, queryParams);
        return GetResourceByIdAsync<Song>(request, cancellationToken);
    }

    #endregion

    #region SongDatas

    /// <summary>
    /// Retrieve all song datas.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<SongData>> GetSongDatasAsync(PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new AllResourcesRequest(Resource.SongData, queryParams);
        return GetResourcesAsync<SongData>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve a song data by id.
    /// </summary>
    /// <param name="songId"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<SongData> GetSongDataByIdAsync(int songId, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new ResourceByIdRequest(Resource.SongData, songId, queryParams);
        return GetResourceByIdAsync<SongData>(request, cancellationToken);
    }

    #endregion

    #region Users

    /// <summary>
    /// Retrieve all users.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<User> GetUserByIdAsync(long userId, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.Users, QueryableColumn.Uid, userId, queryParams);
        return (await QueryResourcesAsync<User>(request, cancellationToken)).FirstOrDefault();
    }

    /// <summary>
    /// Retrieve a user by username.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<User> GetUserByUsernameAsync(string username, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new QueryResourcesRequest(Resource.Users, QueryableColumn.Username, username, queryParams);
        return (await QueryResourcesAsync<User>(request, cancellationToken)).FirstOrDefault();
    }

    #endregion

    #region Venues

    /// <summary>
    /// Retrieve all venues.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Venue>> GetVenuesAsync(PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new AllResourcesRequest(Resource.Venues, queryParams);
        return GetResourcesAsync<Venue>(request, cancellationToken);
    }

    /// <summary>
    /// Retrieve a venue by id.
    /// </summary>
    /// <param name="venueId"></param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Venue> GetVenueByIdAsync(int venueId, PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new())
    {
        var request = new ResourceByIdRequest(Resource.Venues, venueId, queryParams);
        return GetResourceByIdAsync<Venue>(request, cancellationToken);
    }

    #endregion

    /// <summary>
    /// Retrieve resources.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task<List<T>> GetResourcesAsync<T>(
        AllResourcesRequest request,
        CancellationToken cancellationToken = new())
        where T : IPhishNetApiResource
        => GetListAsync<T>(request, cancellationToken);

    /// <summary>
    /// Retrieve a resource by id.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task<T> GetResourceByIdAsync<T>(
        ResourceByIdRequest request,
        CancellationToken cancellationToken = new())
        where T : IPhishNetApiResource
        => (await GetListAsync<T>(request, cancellationToken)).FirstOrDefault();

    /// <summary>
    /// Query resources.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task<List<T>> QueryResourcesAsync<T>(
        QueryResourcesRequest request,
        CancellationToken cancellationToken = new())
        where T : IPhishNetApiResource
        => GetListAsync<T>(request, cancellationToken);

    private async Task<List<T>> GetListAsync<T>(
        ResourceRequest resourceRequest,
        CancellationToken cancellationToken = new())
        where T : IPhishNetApiResource
    {
        try
        {
            var httpRequest = new HttpRequestMessage { Method = HttpMethod.Get, RequestUri = resourceRequest.GetRequestUri(_config) };

            var response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseContentRead, cancellationToken);
            switch (response.StatusCode)
            {
                case < HttpStatusCode.BadRequest:
                    break;
                case HttpStatusCode.NotFound:
                    return [];
                default:
                    throw new PhishNetApiException(response.ReasonPhrase);
            }

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
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
