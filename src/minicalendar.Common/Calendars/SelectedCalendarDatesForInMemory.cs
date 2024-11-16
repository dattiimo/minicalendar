using Microsoft.Extensions.Logging;

namespace minicalendar.Common.Calendars;

public class SelectedCalendarDatesForInMemory(ILogger<SelectedCalendarDatesForInMemory> logger) : ISelectedCalendarDates
{
    private readonly HashSet<DateOnly> _dates = [];

    public async Task<DateOnly[]> GetAsync()
    {
        logger.LogInformation("Get selected dates");
        return await Task.FromResult(_dates.ToArray());
    }

    public async Task<bool> ContainsAsync(DateOnly date)
    {
        logger.LogInformation("Check selected dates contains date");
        return await Task.FromResult(_dates.Contains(date));
    }

    public Task AddAsync(DateOnly date)
    {
        logger.LogInformation("Add selected date {date}", date);
        _dates.Add(date);
        return Task.CompletedTask;
    }

    public Task AddAsync(IEnumerable<DateOnly> dates)
    {
        logger.LogInformation("Add selected dates");
        foreach (var date in dates)
        {
            _dates.Add(date);
        }

        return Task.CompletedTask;
    }

    public Task RemoveAsync(DateOnly date)
    {
        logger.LogInformation("Remove selected date {date}", date);
        _dates.Remove(date);
        return Task.CompletedTask;
    }

    public Task ClearAsync()
    {
        logger.LogInformation("Clear selected dates");
        _dates.Clear();
        return Task.CompletedTask;
    }

}