namespace PhishNet.Requests;

public class GetResourcesRequest : IResourceRequest
{
    public GetResourcesRequest(Resource resource)
    {
        Resource[] supportedResources =
        [
            Resource.Artists,
            Resource.Shows,
            Resource.Songs,
            Resource.SongData,
            Resource.Venues,
        ];
        if (!supportedResources.Contains(resource))
            throw new PhishNetApiException($"GetAllRequest does not support resource '{resource}'.");
        Resource = resource;
    }

    public Resource Resource { get; }

    public string Path => $"{Resource}".ToLower();
}