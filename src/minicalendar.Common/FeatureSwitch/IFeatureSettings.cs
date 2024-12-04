namespace minicalendar.Common.FeatureSwitch;

public interface IFeatureSettings
{
    public Task<bool> IsFeatureEnabledAsync(string featureName);
}