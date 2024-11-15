namespace minicalendar.Common.Calendars;

public class CalendarMonthTrack : ITrack
{
    public bool IsAlternate(DateOnly date)
    {
        return date.Month % 2 == 0;
    }
}