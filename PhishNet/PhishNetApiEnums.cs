namespace PhishNet;

public enum Resource
{
    Artists,
    Attendance,
    JamCharts,
    Reviews,
    Setlists,
    Shows,
    SongData,
    Songs,
    Users,
    Venues,
}

public enum QueryableColumn
{
    Uid,
    Username,
    ShowId,
    ShowDate,
    Slug,
}

public enum SortDirection
{
    Asc,
    Desc,
}