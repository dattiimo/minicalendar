namespace minicalendar.Common.Calendars.Publishing;

public interface ICalendarStorage
{
    public Task<Calendar> GetAsync(Guid id);
    public Task UpdateAsync(Calendar calendar);
}