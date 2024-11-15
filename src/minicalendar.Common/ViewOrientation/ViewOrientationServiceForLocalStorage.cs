using Blazored.LocalStorage;

namespace minicalendar.Common.ViewOrientation;

public class ViewOrientationServiceForLocalStorage(ILocalStorageService localStorage) : IViewOrientationService
{
    private const string Key = "view-orientation";

    public async Task<ViewOrientationType> GetOrientationAsync()
    {
        var view = await localStorage.GetItemAsync<string>(Key);
        return Enum.TryParse(view, true, out ViewOrientationType orientation) ? orientation : ViewOrientationType.Default;
    }
}