namespace minicalendar.Common.Core;

public static class DateOnlyExtensions
{
    private const string IsoDateOnlyFormat = "yyyy-MM-dd";
    
    public static DateOnlySpan Difference(this DateOnly src, DateOnly date)
    {
        var diff = new DateOnlySpan
        {
            Comparative = Comparative(src, date)
        };

        switch (diff.Comparative)
        {
            case DateOnlyComparative.Equal:
            {
                return diff;
            }
            case DateOnlyComparative.Before:
            {
                diff.TotalDays = src.DayNumber - date.DayNumber;
                diff.Weeks = diff.TotalDays / 7;
                diff.Days = diff.TotalDays % 7;
                return diff;
            }
            case DateOnlyComparative.After:
            {
                diff.TotalDays = date.DayNumber - src.DayNumber;
                diff.Weeks = diff.TotalDays / 7;
                diff.Days = diff.TotalDays % 7;
                return diff;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static DateOnlyComparative Comparative(this DateOnly src, DateOnly date)
    {
        if (src == date)
        {
            return DateOnlyComparative.Equal;
        }
        return date > src ? DateOnlyComparative.After : DateOnlyComparative.Before;
    }
    
    public static string ToIsoDateString(this DateOnly source)
    {
        return source.ToString(IsoDateOnlyFormat);
    }

    public static DateOnly GetPreviousDayOfWeek(this DateOnly source, DayOfWeek dayOfWeek)
    {
        var date = source;
        while (date.DayOfWeek != dayOfWeek)
        {
            date = date.AddDays(-1);
        }

        return date;
    }
    
    public static DateOnly GetNextDayOfWeek(this DateOnly source, DayOfWeek dayOfWeek)
    {
        var date = source;
        while (date.DayOfWeek != dayOfWeek)
        {
            date = date.AddDays(1);
        }

        return date;
    }

    public static bool IsWeekend(this DateOnly source)
    {
        return source.DayOfWeek == DayOfWeek.Saturday || source.DayOfWeek == DayOfWeek.Sunday;
    }

    public static DateOnly StartOfMonth(this DateOnly source)
    {
        return new DateOnly(source.Year, source.Month, 1);
    }

    public static DateOnly StartOfYear(this DateOnly source)
    {
        return new DateOnly(source.Year, 1, 1);
    }

    public static bool IsSameMonthAndYear(this DateOnly source, DateOnly date)
    {
        return source.Month == date.Month && source.Year == date.Year;
    }

    public static bool IsBetween(this DateOnly src, DateOnly from, DateOnly to)
    {
        return src >= from && src <= to;
    }
}
