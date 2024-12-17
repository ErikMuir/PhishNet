using Microsoft.AspNetCore.WebUtilities;
using PhishNetApiV5Wrapper.Model;
using System.Security.Authentication;
using System.Text.Json;

namespace PhishNetApiV5Wrapper;

public class Client
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public Client(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new AuthenticationException("API key is required.");
        }

        _apiKey = apiKey;
        _httpClient = new HttpClient { BaseAddress = new Uri("https://api.phish.net/v5/") };
    }

    public Client() : this(Environment.GetEnvironmentVariable("PHISH_NET_API_PRIVATE_KEY")) { }

    #region Artists

    public async Task<List<Artist>> GetArtistsAsync(CancellationToken cancellationToken = new())
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(QueryHelpers.AddQueryString("artists.json", GetBaseParams())),
        };
        return await GetListAsync<Artist>(request, cancellationToken);
    }

    public async Task<Artist> GetArtistByIdAsync(string id, CancellationToken cancellationToken = new())
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(QueryHelpers.AddQueryString($"artists/{id}.json", GetBaseParams())),
        };
        return await GetSingleAsync<Artist>(request, cancellationToken);
    }

    #endregion

    #region Songs

    public async Task<List<Song>> GetSongsAsync(CancellationToken cancellationToken = new())
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(QueryHelpers.AddQueryString("songs.json", GetBaseParams())),
        };
        return await GetListAsync<Song>(request, cancellationToken);
    }

    public async Task<Song> GetSongByIdAsync(string id, CancellationToken cancellationToken = new())
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(QueryHelpers.AddQueryString($"songs/{id}.json", GetBaseParams())),
        };
        return await GetSingleAsync<Song>(request, cancellationToken);
    }

    #endregion

    private Dictionary<string, string> GetBaseParams() => new() { ["apikey"] = _apiKey };

    private Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken);
    }

    private async Task<List<T>> GetListAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = new())
        where T : class
    {
        var response = await SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var dataResponse = JsonSerializer.Deserialize<DataResponse<T>>(content);
        if (dataResponse is null) throw new JsonException("Failed to deserialize response.");
        if (dataResponse.Error) throw new Exception(dataResponse.ErrorMessage);
        return dataResponse.Data;
    }

    private async Task<T> GetSingleAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = new())
        where T : class
    {
        return (await GetListAsync<T>(request, cancellationToken)).FirstOrDefault();
    }
}
