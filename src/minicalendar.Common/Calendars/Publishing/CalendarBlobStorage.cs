using System.Text.Json;
using Azure.Storage.Blobs;

namespace minicalendar.Common.Calendars.Publishing;

public class CalendarStorageForBlobStorage(IBlobStorageConnectionString connectionString) : ICalendarStorage
{
    public async Task UpdateAsync(Calendar calendar)
    {
        var jsonData = JsonSerializer.Serialize(calendar);
        var exportAsBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
        var content = new BinaryData(exportAsBytes);

        var blob = await GetBlobClient(GetFileName(calendar.Id));

        await blob.UploadAsync(content, overwrite: true);
    }

    public async Task<Calendar> GetAsync(Guid id)
    {
        var blob = await GetBlobClient(GetFileName(id));
        var response = await blob.DownloadAsync();

        var calendar = JsonSerializer.Deserialize<Calendar>(response.Value.Content);
        if (calendar == null)
        {
            return Calendar.Empty;
        }
        return calendar;
    }
    
    public async Task RemoveAsync(Guid id)
    {
        var blob = await GetBlobClient(GetFileName(id));

        var exists = await blob.ExistsAsync();
        if (!exists)
        {
            return;
        }

        await blob.DeleteAsync();
    }

    /// <summary>
    /// Returns the filename used to save the calendar
    /// </summary>
    private static string GetFileName(Guid id)
    {
        // Use the Calendar Id for easy lookup. Prefixed with "cal" to make it obvious what the file is when viewing in Azure portal.
        return $"cal_{id}.json";
    }

    private async Task<BlobClient> GetBlobClient(string fileName)
    {
        var connString = await connectionString.GetConnectionStringAsync();
        // Calendars are stored in the "calendars" container
        var container = new BlobContainerClient(connString, "calendars");
        var blobClient = container.GetBlobClient(fileName);
        return blobClient;
    }
}