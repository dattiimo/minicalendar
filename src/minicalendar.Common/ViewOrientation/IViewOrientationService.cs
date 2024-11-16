namespace minicalendar.Common.ViewOrientation;

public interface IViewOrientationService
{
    Task<ViewOrientationType> GetOrientationAsync();
}