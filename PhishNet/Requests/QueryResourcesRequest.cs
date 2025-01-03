namespace PhishNet.Requests;

public class QueryResourcesRequest : ResourceRequest
{
    public QueryResourcesRequest(Resource resource, QueryableColumn column, object value, PhishNetApiQueryParams queryParams = null) : base(resource, queryParams)
    {
        Resource[] supportedResources =
        [
            Resource.Attendance,
            Resource.Reviews,
            Resource.Setlists,
            Resource.JamCharts,
            Resource.Users,
        ];
        if (!supportedResources.Contains(resource))
            throw new PhishNetApiException($"QueryRequest does not support resource '{resource}'.");

        Column = column;
        Value = column switch
        {
            QueryableColumn.Slug => value is string ? value : throw new PhishNetApiException("Invalid value type."),
            QueryableColumn.Uid => value is long ? value : throw new PhishNetApiException("Invalid value type."),
            QueryableColumn.Username => value is string ? value : throw new PhishNetApiException("Invalid value type."),
            QueryableColumn.ShowId => value is long ? value : throw new PhishNetApiException("Invalid value type."),
            QueryableColumn.ShowDate => value is DateOnly ? value : throw new PhishNetApiException("Invalid value type."),
            _ => throw new PhishNetApiException("Invalid column."),
        };
    }

    public QueryableColumn Column { get; }

    public object Value { get; }

    private string ValueString => Column switch
    {
        QueryableColumn.ShowDate => ((DateOnly)Value).ToString("yyyy-MM-dd"),
        _ => Value.ToString(),
    };

    public override string Path => $"{Resource}/{Column}/{ValueString}".ToLower();
}