namespace minicalendar.Common.ViewOrientation;

public class ViewOrientationServiceForServer : IViewOrientationService
{
    public async Task<ViewOrientationType> GetOrientationAsync()
    {
        return await Task.FromResult(ViewOrientationType.Portrait);
    }
}