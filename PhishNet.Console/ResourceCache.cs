using System.Text.Json;

namespace PhishNet.Console;

public interface IResourceCache
{
    public string FilePath { get; }
    public void Save();
}

public class ResourceCache<T> : IResourceCache where T : class, IPhishNetApiResource, new()
{
    public ResourceCache()
    {
        FilePath = $"{Cache.DirName}/{typeof(T).Name}.json";
        if (File.Exists(FilePath))
        {
            Data = JsonSerializer.Deserialize<Dictionary<string, List<T>>>(File.ReadAllText(FilePath));
        }
        else
        {
            Data = new Dictionary<string, List<T>>();
            Save();
        }
    }

    public string FilePath { get; }

    public Dictionary<string, List<T>> Data { get; }

    public void Save()
    {
        File.WriteAllText(FilePath, JsonSerializer.Serialize(Data));
    }
}
