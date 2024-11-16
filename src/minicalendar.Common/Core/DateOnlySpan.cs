namespace minicalendar.Common.Core;

public class DateOnlySpan
{
    public DateOnlyComparative Comparative { get; set; } = DateOnlyComparative.Equal;
    public int Days { get; set; }
    public int Weeks { get; set; }
    public int TotalDays { get; set; }
}
