using Blazored.LocalStorage;

namespace minicalendar.Common.Calendars.Publishing;

public class BlobStorageConnectionStringFromLocalStorage(ILocalStorageService localStorage) : IBlobStorageConnectionString
{
    /// <summary>
    /// Return a connection string for a blob storage instance.
    /// To prevent exposing the blob storage container publicly for PWA usage,
    /// the connection string is store locally within my browser for now.
    /// </summary>
    public async Task<string> GetConnectionStringAsync()
    {
        var connString = await localStorage.GetItemAsync<string>("BlobStorageConnectionString");
        return connString ?? string.Empty;
    }
}