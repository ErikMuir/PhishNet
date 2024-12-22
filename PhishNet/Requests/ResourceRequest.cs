using Microsoft.AspNetCore.WebUtilities;

namespace PhishNet.Requests;

public abstract class ResourceRequest(Resource resource, PhishNetApiQueryParams queryParams = null)
{
    public Resource Resource { get; } = resource;

    public PhishNetApiQueryParams QueryParams { get; } = queryParams ?? new PhishNetApiQueryParams();

    public virtual string Path => $"{Resource}".ToLower();

    public Uri GetRequestUri(PhishNetApiClientConfig config)
    {
        var url = $"{config.BaseUrl}/{Path}.{config.Format}";
        QueryParams.ApiKey = config.ApiKey;
        return new Uri(QueryHelpers.AddQueryString(url, QueryParams.ToDictionary()));
    }
}