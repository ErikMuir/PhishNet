namespace PhishNet.Console;

public class CacheData<T>(List<T> items, DateTime? expiration = null) where T : IPhishNetApiResource
{
    public DateTime Expiration { get; set; } = expiration ?? DateTime.Now.AddHours(1);

    public List<T> Items { get; set; } = items;
}
