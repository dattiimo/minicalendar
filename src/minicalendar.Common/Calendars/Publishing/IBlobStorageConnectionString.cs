namespace minicalendar.Common.Calendars.Publishing;

public interface IBlobStorageConnectionString
{
    public Task<string> GetConnectionStringAsync();
}