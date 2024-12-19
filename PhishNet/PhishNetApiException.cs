namespace PhishNet;

public class PhishNetApiException : Exception
{
    public PhishNetApiException() { }

    public PhishNetApiException(string message) : base(message) { }

    public PhishNetApiException(string message, Exception innerException) : base(message, innerException) { }
}