namespace PhishNetApiV5Wrapper;

public class PhishNetApiClientConfig
{
    public string ApiKey { get; set; } = Environment.GetEnvironmentVariable("PHISH_NET_API_KEY");

    public string BaseUrl { get; set; } = "https://api.phish.net/v5";

    public string Format { get; set; } = "json";

    public bool LogsEnabled { get; set; }
}