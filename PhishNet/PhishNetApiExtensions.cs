namespace PhishNet;

public static class PhishNetApiExtensions
{
    public static bool ToBool(this int value) => value != 0;

    public static bool ToBool(this int? value) => value.HasValue && value.Value != 0;

    public static DateTime? ToDateTime(this string value, DateTime? defaultValue = null) =>
        DateTime.TryParse(value, out var result) ? result : defaultValue;

    public static DateOnly? ToDateOnly(this string value, DateOnly? defaultValue = null) =>
        DateOnly.TryParse(value, out var result) ? result : defaultValue;
}