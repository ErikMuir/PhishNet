namespace PhishNet.Requests;

public class ResourceByIdRequest : ResourceRequest
{
    public ResourceByIdRequest(Resource resource, long id, PhishNetApiQueryParams queryParams = null) : base(resource, queryParams)
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
            throw new PhishNetApiException($"GetByIdRequest does not support resource '{resource}'.");

        Id = id;
    }

    public long Id { get; }

    public override string Path => $"{Resource}/{Id}".ToLower();
}