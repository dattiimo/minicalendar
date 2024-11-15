namespace minicalendar.Common.Calendars;

public interface ISelectedCalendarDates
{
    public Task<DateOnly[]> GetAsync();
    public Task<bool> ContainsAsync(DateOnly date);
    public Task AddAsync(DateOnly date);
    public Task AddAsync(IEnumerable<DateOnly> date);
    public Task RemoveAsync(DateOnly date);
    public Task ClearAsync();

}