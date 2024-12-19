namespace PhishNet;

public static class PhishNetApiExtensions
{
    public static bool ToBool(this int value) => value != 0;

    public static bool ToBool(this int? value) => value.HasValue && value.Value != 0;

    public static DateTime ToDateTime(this string value) =>
        DateTime.TryParse(value, out var result) ? result : DateTime.MinValue;

    public static DateTime? ToNullableDateTime(this string value) =>
        DateTime.TryParse(value, out var result) ? result : null;

    public static DateOnly ToDateOnly(this string value) =>
        DateOnly.TryParse(value, out var result) ? result : DateOnly.MinValue;

    public static DateOnly? ToNullableDateOnly(this string value) =>
        DateOnly.TryParse(value, out var result) ? result : null;
}