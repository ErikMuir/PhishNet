using System.Text.Json;

namespace PhishNet.Console;

public class Cache<T> where T : class, ICacheData, new()
{
    private const string _dirName = "cache";
    private readonly string _filePath;

    public Cache(string filePath = "data.json")
    {
        _filePath = filePath;
        Data = File.Exists(_filePath) ? JsonSerializer.Deserialize<T>(File.ReadAllText(_filePath)) : new();
    }

    public void Save()
    {
        File.WriteAllText(_filePath, JsonSerializer.Serialize(Data));
    }

    public T Data { get; set; }
}
