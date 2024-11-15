namespace minicalendar.Common.Privacy.CookiePolicy;

public interface ICookiePolicyManager
{
    public Task<bool> HasAcceptedFunctionalCookiesAsync(); 

    public Task<bool> HasAcceptedAnalyticsCookiesAsync();

    public Task<CookiePolicyPreferences> GetPreferencesAsync();

    public Task SetPreferencesAsync(CookiePolicyPreferences preferences);

    public Task AcceptAllCookiesAsync();

    public Task RejectAllCookiesAsync();

    public Task AcceptFunctionalCookiesAsync();

    public Task RejectFunctionalCookiesAsync();

    public Task AcceptAnalyticalCookiesAsync();

    public Task RejectAnalyticalCookiesAsync();

}