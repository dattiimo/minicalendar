namespace minicalendar.Common.Calendars;

public class TrackFactory
{
    public ITrack CreateTrack(bool enableSprint, DateOnly? startDate, int? unit)
    {
        if (!enableSprint) 
            return new CalendarMonthTrack();
        
        if (startDate == null || unit == null)
        {
            return new CalendarMonthTrack(); // default to calendar month
        }
        return new SprintTrack(startDate.Value, unit.Value);
    }
}