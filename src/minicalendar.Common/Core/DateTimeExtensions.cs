namespace minicalendar.Common.Core;

public static class DateTimeExtensions
{
    private const string IsoDateOnlyFormat = "yyyy-MM-dd";

    public static string ToIsoDateString(this DateTime source)
    {
        return source.ToString(IsoDateOnlyFormat);
    }

    public static DateOnly ToDateOnly(this DateTime source)
    {
        return new DateOnly(source.Year, source.Month, source.Day);
    }
}