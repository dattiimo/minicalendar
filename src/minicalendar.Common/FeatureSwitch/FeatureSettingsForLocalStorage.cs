using System.Collections.Concurrent;
using Blazored.LocalStorage;

namespace minicalendar.Common.FeatureSwitch;

public class FeatureSettingsForLocalStorage(ILocalStorageService localStorage) : IFeatureSettings
{
    private readonly ConcurrentDictionary<string, bool> _cachedLookup = new();

    public async Task<bool> IsFeatureEnabledAsync(string featureName)
    {
        var cacheKey = featureName.ToLowerInvariant();
        if (_cachedLookup.TryGetValue(cacheKey, out var value))
        {
            return value;
        }

        var localValue = await localStorage.GetItemAsync<bool?>(GetKeyName(featureName));
        _cachedLookup.TryAdd(cacheKey, localValue == true);
        return localValue == true;
    }

    private static string GetKeyName(string featureName)
    {
        return $"Common_FeatureSwitch_{featureName.ToLowerInvariant()}";
    }
}