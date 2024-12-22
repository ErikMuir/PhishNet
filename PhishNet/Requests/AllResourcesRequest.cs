namespace PhishNet.Requests;

public class AllResourcesRequest : ResourceRequest
{
    public AllResourcesRequest(Resource resource, PhishNetApiQueryParams queryParams = null) : base(resource, queryParams)
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
    }

    public override string Path => $"{Resource}".ToLower();
}