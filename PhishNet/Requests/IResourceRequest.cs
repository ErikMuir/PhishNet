namespace PhishNet.Requests;

public interface IResourceRequest
{
    public Resource Resource { get; }
    public string Path { get; }
}