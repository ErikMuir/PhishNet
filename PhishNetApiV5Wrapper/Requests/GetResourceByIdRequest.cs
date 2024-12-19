namespace PhishNetApiV5Wrapper.Requests;

public class GetResourceByIdRequest : IResourceRequest
{
    public GetResourceByIdRequest(Resource resource, long id)
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
        Resource = resource;
        Id = id;
    }

    public Resource Resource { get; }

    public long Id { get; }

    public string Path => $"{Resource}/{Id}".ToLower();
}