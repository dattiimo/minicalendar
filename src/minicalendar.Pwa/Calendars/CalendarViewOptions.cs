using minicalendar.Common.ViewOrientation;

namespace minicalendar.Pwa.Calendars;

public class CalendarViewOptions
{
    public ViewOrientationType Orientation { get; set; } = ViewOrientationType.Default;
    public bool ShowLegend { get; set; } = true;
    public bool ShowDates { get; set; } = true;
    public bool ShowSprints { get; set; }
    public DateOnly SprintStartDate { get; set; }
    public int SprintLengthInDays { get; set; }
}