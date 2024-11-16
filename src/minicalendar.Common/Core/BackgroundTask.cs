namespace minicalendar.Common.Core;

public class BackgroundTask(TimeSpan interval, Func<Task> eventCallback) : IDisposable
{
    private Task? _timerTask;
    private readonly PeriodicTimer _timer = new(interval);

    public void Start()
    {
        _timerTask = RunAsync();
    }

    private async Task RunAsync()
    {
        try
        {
            while (await _timer.WaitForNextTickAsync())
            {
                await eventCallback.Invoke();
            }
        }
        catch (OperationCanceledException)
        {

        }
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}