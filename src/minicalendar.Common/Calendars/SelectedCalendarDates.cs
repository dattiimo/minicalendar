using Blazored.LocalStorage;

namespace minicalendar.Common.Calendars;

public class SelectedCalendarDates(ILocalStorageService localStorageService) : ISelectedCalendarDates
{
    //private Dictionary<DateOnly, bool> _dates = new();
    private const string StorageKey = "SelectedCalendarDates";

    private async Task<List<DateOnly>> GetDataAsync()
    {
        Console.WriteLine($"load");
        //_dates = new Dictionary<DateOnly, bool>();
        var dates = await localStorageService.GetItemAsync<List<DateOnly>>(StorageKey);
        Console.WriteLine($"selected: {dates}");
        // var selected = saved.Split('|');
        // foreach (var sel in selected)
        // {
        //     if (DateOnly.TryParse(sel, out var date))
        //     {
        //         await AddAsync(date);
        //     }
        // }
        // if (string.IsNullOrEmpty(saved))
        // {
        //     return new DateOnly[];
        // }
        return dates ?? [];
    }

    public async Task<DateOnly[]> GetAsync()
    {
        var dates = await GetDataAsync();
        return await Task.FromResult(dates.ToArray());
    }

    public async Task SetAsync(IEnumerable<DateOnly> dates)
    {
        await localStorageService.SetItemAsync(StorageKey, dates.ToList());
    }
    
    // private async Task UpdateToStoreAsync()
    // {
    //     var selected = string.Join("|", _dates.Keys.Select(x => x.ToString("yyyy-MM-dd")));
    //     Console.WriteLine("UpdateToStore: " + selected);
    //     await localStorageService.SetItemAsync("selected", selected);
    // }

    public async Task<bool> ContainsAsync(DateOnly date)
    {
        var dates = await GetDataAsync();
        return dates.Contains(date);
    }

    public async Task AddAsync(DateOnly date)
    {
        var dates = await GetDataAsync();
        if (dates.Contains(date))
        {
            return;
        }

        dates.Add(date);
        await SetAsync(dates);

        // if (_dates.ContainsKey(date)) 
        //     return;
        // Console.WriteLine($"selected add: {date}");
        // _dates.Add(date, true);
        // await UpdateToStoreAsync();
    }
    
    public async Task AddAsync(IEnumerable<DateOnly> dates)
    {
        var existingDates = await GetDataAsync();

        foreach (var date in dates)
        {
            if (existingDates.Contains(date))
            {
                continue;
            }

            existingDates.Add(date);
        }

        await SetAsync(existingDates);
    }

    public async Task RemoveAsync(DateOnly date)
    {
        var dates = await GetDataAsync();
        dates.Remove(date);
        await SetAsync(dates);

        // if (!_dates.ContainsKey(date)) 
        //     return;
        // Console.WriteLine($"selected remove: {date}");
        // _dates.Remove(date);
        // await UpdateToStoreAsync();
    }

    // public async Task LoadAsync()
    // {
    //     Console.WriteLine($"load");
    //     _dates = new Dictionary<DateOnly, bool>();
    //     var saved = await localStorageService.GetItemAsync<string>("selected");
    //     Console.WriteLine($"selected: {saved}");
    //     if (string.IsNullOrEmpty(saved))
    //     {
    //         return;
    //     }
    //
    //     var selected = saved.Split('|');
    //     foreach (var sel in selected)
    //     {
    //         if (DateOnly.TryParse(sel, out var date))
    //         {
    //             await AddAsync(date);
    //         }
    //     }
    // }

    // public async Task<List<DateOnly>> GetDatesAsync()
    // {
    //     var dates = await GetAsync();
    //     return dates.ToList();
    // }

    // public List<DateOnly> GetDates()
    // {
    //     
    //     var dates = _dates.Keys.Select(d => d).ToList();
    //     Console.WriteLine($"SelectedDatesRepo GetDates {string.Join(",", dates)}");
    //     return dates;
    // }

    public async Task ClearAsync()
    {
        await SetAsync([]);
        // _dates = new Dictionary<DateOnly, bool>();
        // Console.WriteLine("SelectedDatesRepo clearing dates");
        // await UpdateToStoreAsync();
        // Console.WriteLine("SelectedDatesRepo dates cleared");
    }

    public async Task<bool> AnyAsync()
    {
        var dates = await GetDataAsync();
        return dates.Count > 0;
    }

    // public override string ToStringAsync()
    // {
    //     //return string.Join(',', GetDatesAsync());
    // }

}