using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

namespace minicalendar.Common.Calendars;

public class LocalStorageCalendarRepository(
    ILogger<LocalStorageCalendarRepository> logger,
    ILocalStorageService localStorage,
    TimeProvider timeProvider) : ICalendarRepository
{
    private async Task<IEnumerable<string>> GetAllKeysAsync()
    {
        var keys = await localStorage.KeysAsync();
        var calKeys = keys.Where(x => x.StartsWith("Cal_"));
        return await Task.FromResult(calKeys.ToArray());
    }
    
    public async Task<List<Calendar>> GetAllAsync()
    {
        var keys = await GetAllKeysAsync();

        var data = new List<Calendar>();
        foreach (var key in keys)
        {
            var cal = await localStorage.GetItemAsync<Calendar>(key);
            if (cal != null)
            {
                data.Add(cal);
            }
        }

        var orderedData = data.OrderBy(x => x.Title).ToList();
        return await Task.FromResult(orderedData);
    }

    public async Task<bool> ContainsIdAsync(Guid id)
    {
        var result = await ContainsKeyAsync(GetKey(id));
        return await Task.FromResult(result);
    }

    private async Task<bool> ContainsKeyAsync(string key)
    {
        var result = await localStorage.ContainKeyAsync(key);
        return await Task.FromResult(result);
    }
    
    public async Task<Calendar> GetByIdAsync(Guid id)
    {
        var key = GetKey(id);
        if (!await ContainsKeyAsync(key))
        {
            return Calendar.Empty;
        }

        var cal = await localStorage.GetItemAsync<Calendar>(key);
        if (cal == null)
        {
            return Calendar.Empty;
        }

        if (cal.Activities.Count == 0 && cal.Events.Count > 0)
        {
            cal.Activities = cal.Events;
        }
        return await Task.FromResult(cal);
    }

    public async Task SaveItemAsync(Calendar cal)
    {
        cal.LastModifiedAt = DateTime.UtcNow;
        if (!cal.EnableSprint)
        {
            cal.SprintInterval = null;
            cal.SprintStartDate = null;
        }
        await CommitItemAsync(cal);
    }

    private async Task CommitItemAsync(Calendar cal)
    {
        var key = GetKey(cal.Id);
        await localStorage.SetItemAsync(key, cal);
    }

    public async Task RecordView(Calendar cal)
    {
        cal.LastViewedAt = timeProvider.GetLocalNow().DateTime;
        await CommitItemAsync(cal);
    }

    public async Task RemoveByIdAsync(Guid id)
    {
        logger.LogInformation($"Remove calendar id:{id}");
        if (!await ContainsIdAsync(id))
        {
            throw new Exception($"Item not found id:{id}");
        }

        // if (await GetCountAsync() <= 1)
        // {
        //     throw new Exception("There must be always be at least one calendar");
        // } 

        await localStorage.RemoveItemAsync(GetKey(id));
    }

    private async Task<int> GetCountAsync()
    {
        var all = await GetAllKeysAsync();
        return all.Count();
    }

    private static string GetKey(Guid id) => $"Cal_{id}";

}