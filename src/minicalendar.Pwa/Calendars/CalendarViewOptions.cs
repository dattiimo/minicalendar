using minicalendar.Common.ViewOrientation;

namespace minicalendar.Pwa.Calendars;

public class CalendarViewOptions
{
    public ViewOrientationType Orientation { get; set; } = ViewOrientationType.Default;
    public bool ShowLegend { get; set; } = true;
    public bool DisplayDates { get; set; } = true;
}