namespace PhishNet;

public class PhishNetApiQueryParams
{
    public string OrderBy { get; set; }

    public SortDirection Direction { get; set; } = SortDirection.Asc;

    public int? Limit { get; set; }

    public string ApiKey { get; set; }

    public Dictionary<string, string> ToDictionary()
    {
        var dict = new Dictionary<string, string>();

        if (!string.IsNullOrWhiteSpace(OrderBy))
            dict.Add("order_by", OrderBy);
        if (Direction == SortDirection.Desc)
            dict.Add("direction", "desc");
        if (Limit.HasValue)
            dict.Add("limit", $"{Limit}");
        if (!string.IsNullOrWhiteSpace(ApiKey))
            dict.Add("apikey", ApiKey);

        return dict;
    }
}