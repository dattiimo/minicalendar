using Blazored.LocalStorage;

namespace minicalendar.Pwa.Calendars;

public class CalendarViewOptionsStore(ILocalStorageService localStorage)
{
    private const string StoreKey = "Pwa_Calendars_CalendarViewOptions";
    
    public async Task SaveAsync(CalendarViewOptions viewOptions)
    {
        await localStorage.SetItemAsync(StoreKey, viewOptions);
    }
    
    public async Task<CalendarViewOptions> GetAsync()
    {
        var containsKey = await localStorage.ContainKeyAsync(StoreKey);
        if (!containsKey)
            return new CalendarViewOptions();
        var options = await localStorage.GetItemAsync<CalendarViewOptions>(StoreKey);
        return options ?? new CalendarViewOptions();
    }
}