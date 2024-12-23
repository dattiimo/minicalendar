using System.ComponentModel.DataAnnotations;
using minicalendar.Common.Calendars.Activities;
using minicalendar.Common.Core;

namespace minicalendar.Common.Calendars;

public class Calendar : IValidatableObject
{
    public static readonly Calendar Empty = new();

    [Key] public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastModifiedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastViewedAt { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "A title is required.")]
    [MaxLength(50, ErrorMessage = "The title must be less than 50 characters.")]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "A start date is required.")]
    public DateOnly StartDate { get; set; } = DateTime.Now.ToDateOnly().StartOfYear();
    public List<Activity> Events { get; set; } = [];
    public List<Activity> Activities { get; set; } = [];
    
    public CalendarPublication? Publication { get; set; }

    public void PopulateFrom(Calendar src)
    {
        CreatedAt = src.CreatedAt;
        LastModifiedAt = src.LastModifiedAt;
        LastViewedAt = src.LastViewedAt;
        Title = src.Title;
        Description = src.Description;
        StartDate = src.StartDate;
    }

    public Calendar Duplicate(string postfix = "")
    {
        var now = DateTime.Now;
        var cal = new Calendar();
        cal.PopulateFrom(this);
        cal.CreatedAt = now;
        cal.LastModifiedAt = now;
        cal.Title = Title + postfix; 
        foreach (var evt in Activities)
        {
            var newActivity = new Activity();
            newActivity.PopulateFrom(evt);
            cal.Activities.Add(newActivity);
        }
        return cal;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        yield break;
    }

    public Task AddActivityAsync(Activity evt)
    {
        Activities.Add(evt);
        return Task.CompletedTask;
    }

    public Task RemoveActivityByIdAsync(Guid id)
    {
        var evt = Activities.SingleOrDefault(x => x.Id == id);
        if (evt == null)
            return Task.CompletedTask;
        Activities.Remove(evt);
        return Task.CompletedTask;
    }

}