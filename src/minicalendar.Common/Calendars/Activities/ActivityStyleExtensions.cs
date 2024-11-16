namespace minicalendar.Common.Calendars.Activities;

public static class ActivityStyleExtensions
{
    private static string GetAltColourStyle(string style, int colourStyle)
    {
        return colourStyle switch
        {
            0 => $"{style}-white",
            1 => $"{style}-green-500",
            2 => $"{style}-purple-500",
            3 => $"{style}-cyan-500",
            4 => $"{style}-indigo-500",
            5 => $"{style}-yellow-500 ",
            6 => $"{style}-red-500",
            _ => string.Empty
        };
    }
    
    public static string GetBackgroundStyle(this Activity source, int style, bool useAltStyle = false)
    {
        return (style == 0 ? "text-gray-900 " : "text-white ") + 
            (useAltStyle ? GetAltColourStyle("bg", style) : 
            GetColourStyle(source, "bg", style));
    }
    
    public static string GetColourStyle(this Activity source, string style, int colourStyle)
    {
        return colourStyle switch
        {
            0 => $"{style}-white",
            1 => $"{style}-green-400",
            2 => $"{style}-purple-400",
            3 => $"{style}-cyan-400",
            4 => $"{style}-indigo-400",
            5 => $"{style}-yellow-400 ",
            6 => $"{style}-red-400",
            _ => string.Empty
        };
    }
}