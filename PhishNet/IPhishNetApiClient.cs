using PhishNet.Models;
using PhishNet.Requests;

namespace PhishNet;

public interface IPhishNetApiClient
{
    public Task<List<Artist>> GetArtistsAsync(PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<Artist> GetArtistByIdAsync(int artistId, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<Attendance>> GetAttendanceByUserIdAsync(long userId, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<Attendance>> GetAttendanceByUsernameAsync(string username,
        PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new());

    public Task<List<Attendance>> GetAttendanceByShowIdAsync(long showId, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<Attendance>> GetAttendanceByShowDateAsync(DateOnly showDate,
        PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new());

    public Task<List<JamChart>> GetJamChartsBySongAsync(string songSlug, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<JamChart>> GetJamChartsByShowIdAsync(long showId, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<JamChart>> GetJamChartsByShowDateAsync(DateOnly showDate,
        PhishNetApiQueryParams queryParams = null, CancellationToken cancellationToken = new());

    public Task<List<Review>> GetReviewsByUserIdAsync(long userId, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<Review>> GetReviewsByUsernameAsync(string username, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<Review>> GetReviewsByShowIdAsync(long showId, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<Review>> GetReviewsByShowDateAsync(DateOnly showDate, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<Setlist>> GetSetlistsBySongAsync(string songSlug, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<Setlist>> GetSetlistsByShowIdAsync(long showId, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<Setlist>> GetSetlistsByShowDateAsync(DateOnly showDate, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<Show>> GetShowsAsync(PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<Show> GetShowByIdAsync(long showId, CancellationToken cancellationToken = new());

    public Task<List<Song>> GetSongsAsync(PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<Song> GetSongByIdAsync(int songId, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<SongData>> GetSongDatasAsync(PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<SongData> GetSongDataByIdAsync(int songId, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<User> GetUserByIdAsync(long userId, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<User> GetUserByUsernameAsync(string username, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<Venue>> GetVenuesAsync(PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<Venue> GetVenueByIdAsync(int venueId, PhishNetApiQueryParams queryParams = null,
        CancellationToken cancellationToken = new());

    public Task<List<T>> GetResourcesAsync<T>(
        AllResourcesRequest request,
        CancellationToken cancellationToken = new())
        where T : IPhishNetApiResource;

    public Task<T> GetResourceByIdAsync<T>(
        ResourceByIdRequest request,
        CancellationToken cancellationToken = new())
        where T : IPhishNetApiResource;

    public Task<List<T>> QueryResourcesAsync<T>(
        QueryResourcesRequest request,
        CancellationToken cancellationToken = new())
        where T : IPhishNetApiResource;
}