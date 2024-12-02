namespace minicalendar.Common.Calendars.Publishing;

public class CalendarPublication(Calendar calendar)
{
    public Guid Id { get; set; } = calendar.Id;
    public Calendar Calendar { get; set; } = calendar;
}