using Blazored.LocalStorage;

namespace minicalendar.Common.Privacy.CookiePolicy;

public class CookiePolicyManagerForLocalStorage(ILocalStorageService localStorage) : ICookiePolicyManager
{
    public async Task<bool> HasAcceptedFunctionalCookiesAsync()
    {
        return (await GetPreferencesAsync()).AcceptFunctionalCookies;
    }

    public async Task<bool> HasAcceptedAnalyticsCookiesAsync()
    {
        return (await GetPreferencesAsync()).AcceptAnalyticalCookies;
    } 

    public async Task AcceptAllCookiesAsync()
    {
        await SetPreferencesAsync(new CookiePolicyPreferences
        {
            AcceptFunctionalCookies = true,
            AcceptAnalyticalCookies = true
        });
    }

    public async Task RejectAllCookiesAsync()
    {
        await SetPreferencesAsync(new CookiePolicyPreferences
        {
            AcceptFunctionalCookies = true,
            AcceptAnalyticalCookies = false
        });
    }

    public async Task AcceptFunctionalCookiesAsync()
    {
        var preferences = await GetPreferencesAsync();
        preferences.AcceptFunctionalCookies = true;
        await SetPreferencesAsync(preferences);
    }

    public async Task RejectFunctionalCookiesAsync()
    {
        var preferences = await GetPreferencesAsync();
        preferences.AcceptFunctionalCookies = false;
        await SetPreferencesAsync(preferences);
    }

    public async Task AcceptAnalyticalCookiesAsync()
    {
        var preferences = await GetPreferencesAsync();
        preferences.AcceptAnalyticalCookies = true;
        await SetPreferencesAsync(preferences);
    }

    public async Task RejectAnalyticalCookiesAsync()
    {
        var preferences = await GetPreferencesAsync();
        preferences.AcceptAnalyticalCookies = false;
        await SetPreferencesAsync(preferences);
    }

    public async Task<CookiePolicyPreferences> GetPreferencesAsync()
    {
        var preferences = new CookiePolicyPreferences
        {
            AcceptFunctionalCookies = await localStorage.GetItemAsync<bool>("cookie-policy"),
            AcceptAnalyticalCookies = await localStorage.GetItemAsync<bool>("cookie-policy:analytics")
        };
        return preferences;
    }

    public async Task SetPreferencesAsync(CookiePolicyPreferences preferences)
    {
        // Acceptance of functional cookies should be implied when making any changes to preferences
        await localStorage.SetItemAsync("cookie-policy", true);
        await localStorage.SetItemAsync("cookie-policy:analytics", preferences.AcceptAnalyticalCookies);
    }
}