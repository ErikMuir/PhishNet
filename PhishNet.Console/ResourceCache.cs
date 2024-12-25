using System.Text.Json;

namespace PhishNet.Console;

public class ResourceCache<T> where T : IPhishNetApiResource
{
    public ResourceCache()
    {
        FilePath = $"{Cache.DirName}/{typeof(T).Name}.json";
        if (File.Exists(FilePath))
        {
            Data = JsonSerializer.Deserialize<Dictionary<string, CacheData<T>>>(File.ReadAllText(FilePath));
        }
        else
        {
            Save();
        }
    }

    public string FilePath { get; }

    public Dictionary<string, CacheData<T>> Data { get; } = [];

    public void Save()
    {
        File.WriteAllText(FilePath, JsonSerializer.Serialize(Data));
    }
}
