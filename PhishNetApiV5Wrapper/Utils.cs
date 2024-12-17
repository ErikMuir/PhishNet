namespace PhishNetApiV5Wrapper;

public static class Utils
{
    public static string GetResourceName(Resources resource)
    {
        return resource switch
        {
            Resources.Artists => "artists",
            Resources.Attendance => "attendance",
            Resources.JamCharts => "jamcharts",
            Resources.Reviews => "reviews",
            Resources.SetLists => "setlists",
            Resources.Shows => "shows",
            Resources.SongData => "songdata",
            Resources.Songs => "songs",
            Resources.Users => "users",
            Resources.Venues => "venues",
            _ => throw new ArgumentOutOfRangeException(nameof(resource), resource, null),
        };
    }
}