namespace minicalendar.Pwa.Calendars.Components;

public class CalendarListItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsEditable { get; set; }
    public string Badge { get; set; } = string.Empty;
    public DateTime? LastModifiedAt { get; set; }
    public bool IsPublished { get; set; }
}