using minicalendar.Common.Calendars;
using minicalendar.Common.Calendars.Activities;
using minicalendar.Common.Core;

namespace minicalendar.Pwa.Calendars.Templates;

public class TemplateCalendarRepository
{
    private readonly List<Calendar> _calendars =
    [
        new Calendar
        {
            Id = new Guid("16765d1d-7797-49af-aec4-c0d54673473d"),
            Title = "New Blank Calendar",
            Description = $"Get started with a new calendar for {DateTime.Now.Year}",
            StartDate = new DateOnly(DateTime.Now.Year, 1, 1),
            EnableSprint = false
        },
        new Calendar
        {
            Id = new Guid("952ef9b9-f398-4dc8-808c-3ecf3ce65ff2"),
            Title = "New Sprint Planner",
            Description = "Sprint planner with two week sprints (edit in settings).",
            StartDate = new DateOnly(DateTime.Now.Year, 1, 1),
            EnableSprint = true,
            SprintStartDate = new DateOnly(DateTime.Now.Year, 1, 1).GetNextDayOfWeek(DayOfWeek.Wednesday),
            SprintInterval = 14
        },
        new Calendar
        { 
            Id = new Guid("796027c5-b597-4237-acc8-3537a48f8e18"),
            Title = "The Eras Tour Demo",
            Description = "The Eras Tour Dates for 2024",
            StartDate = new DateOnly(2024, 1, 1),
            Activities = [
                new Activity
                {
                    Title = "Asia",
                    ColourKind = 2,
                    IsShown = true,
                    Dates = [
                        new DateOnly(2024, 2, 7),
                        new DateOnly(2024, 2, 8),
                        new DateOnly(2024, 2, 9),
                        new DateOnly(2024, 2, 10),
                        new DateOnly(2024, 3, 4),
                        new DateOnly(2024, 3, 7),
                        new DateOnly(2024, 3, 8),
                        new DateOnly(2024, 3, 9)
                    ]
                },
                new Activity
                {
                    Title = "Australia",
                    ColourKind = 5,
                    IsShown = true,
                    Dates = [
                        new DateOnly(2024, 2, 16),
                        new DateOnly(2024, 2, 17),
                        new DateOnly(2024, 2, 23),
                        new DateOnly(2024, 2, 24),
                        new DateOnly(2024, 2, 25),
                        new DateOnly(2024, 2, 26)
                    ]
                },
                new Activity
                {
                    Title = "Europe",
                    ColourKind = 3,
                    IsShown = true,
                    Dates = [
                        new DateOnly(2024, 5, 9),
                        new DateOnly(2024, 5, 10),
                        new DateOnly(2024, 5, 11),
                        new DateOnly(2024, 5, 12),
                        new DateOnly(2024, 5, 17),
                        new DateOnly(2024, 5, 18),
                        new DateOnly(2024, 5, 19),
                        new DateOnly(2024, 5, 24),
                        new DateOnly(2024, 5, 25),
                        new DateOnly(2024, 5, 30),
                        new DateOnly(2024, 6, 2),
                        new DateOnly(2024, 6, 3),
                        new DateOnly(2024, 6, 7),
                        new DateOnly(2024, 6, 8),
                        new DateOnly(2024, 6, 9),
                        new DateOnly(2024, 6, 13),
                        new DateOnly(2024, 6, 14),
                        new DateOnly(2024, 6, 15),
                        new DateOnly(2024, 6, 18),
                        new DateOnly(2024, 6, 21),
                        new DateOnly(2024, 6, 22),
                        new DateOnly(2024, 6, 23),
                        new DateOnly(2024, 6, 28),
                        new DateOnly(2024, 6, 29),
                        new DateOnly(2024, 6, 30),
                        new DateOnly(2024, 7, 4),
                        new DateOnly(2024, 7, 5),
                        new DateOnly(2024, 7, 6),
                        new DateOnly(2024, 7, 9),
                        new DateOnly(2024, 7, 10),
                        new DateOnly(2024, 7, 13),
                        new DateOnly(2024, 7, 14),
                        new DateOnly(2024, 7, 17),
                        new DateOnly(2024, 7, 18),
                        new DateOnly(2024, 7, 19),
                        new DateOnly(2024, 7, 23),
                        new DateOnly(2024, 7, 24),
                        new DateOnly(2024, 7, 27),
                        new DateOnly(2024, 7, 28),
                        new DateOnly(2024, 8, 1),
                        new DateOnly(2024, 8, 2),
                        new DateOnly(2024, 8, 3),
                        new DateOnly(2024, 8, 8),
                        new DateOnly(2024, 8, 9),
                        new DateOnly(2024, 8, 10),
                        new DateOnly(2024, 8, 15),
                        new DateOnly(2024, 8, 16),
                        new DateOnly(2024, 8, 17),
                        new DateOnly(2024, 8, 19),
                        new DateOnly(2024, 8, 2)
                    ]
                },
                new Activity
                {
                    Title = "North America",
                    ColourKind = 4,
                    IsShown = true,
                    Dates = [
                        new DateOnly(2024, 11, 14),
                        new DateOnly(2024, 11, 15),
                        new DateOnly(2024, 11, 16),
                        new DateOnly(2024, 11, 21),
                        new DateOnly(2024, 11, 22),
                        new DateOnly(2024, 11, 23),
                        new DateOnly(2024, 12, 6),
                        new DateOnly(2024, 12, 7),
                        new DateOnly(2024, 12, 8)
                    ]
                },
                new Activity
                {
                    Title = "USA",
                    ColourKind = 1,
                    IsShown = true,
                    Dates = [
                        new DateOnly(2024, 10, 18),
                        new DateOnly(2024, 10, 19),
                        new DateOnly(2024, 10, 20),
                        new DateOnly(2024, 10, 25),
                        new DateOnly(2024, 10, 26),
                        new DateOnly(2024, 10, 27),
                        new DateOnly(2024, 11, 1),
                        new DateOnly(2024, 11, 2),
                        new DateOnly(2024, 11, 3)
                    ]
                }
            ]
        }
    ];

    public Task<List<Calendar>> GetAllAsync()
    {
        return Task.FromResult(_calendars);
    }

    public Task<bool> ContainsIdAsync(Guid id)
    {
        var result = _calendars.Any(x => x.Id == id);
        return Task.FromResult(result);
    }

    public Task<Calendar> GetByIdAsync(Guid id)
    {
        var result = _calendars.SingleOrDefault(x => x.Id == id) ?? Calendar.Empty;
        return Task.FromResult(result);
    }

    public Task SaveItemAsync(Calendar cal)
    {
        throw new NotSupportedException();
    }

    public Task RemoveByIdAsync(Guid id)
    {
        throw new NotSupportedException();
    }

}