namespace minicalendar.Common.Calendars;

public interface ICalendarRepository
{
    public Task<List<Calendar>> GetAllAsync();

    public Task<bool> ContainsIdAsync(Guid id);

    public Task<Calendar> GetByIdAsync(Guid id);

    public Task SaveItemAsync(Calendar cal);

    public Task RecordView(Calendar cal);

    public Task RemoveByIdAsync(Guid id);

}