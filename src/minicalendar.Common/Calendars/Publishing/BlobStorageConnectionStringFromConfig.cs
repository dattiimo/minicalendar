using Microsoft.Extensions.Configuration;

namespace minicalendar.Common.Calendars.Publishing;

public class BlobStorageConnectionStringFromConfig(IConfiguration configuration) : IBlobStorageConnectionString
{
    public async Task<string> GetConnectionStringAsync()
    {
        var connString = configuration["BlobStorageConnectionString"] ?? string.Empty;
        return await Task.FromResult(connString);
    }
}