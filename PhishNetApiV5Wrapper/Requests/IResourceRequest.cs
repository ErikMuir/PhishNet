namespace PhishNetApiV5Wrapper.Requests;

public interface IResourceRequest
{
    public Resource Resource { get; }
    public string Path { get; }
}