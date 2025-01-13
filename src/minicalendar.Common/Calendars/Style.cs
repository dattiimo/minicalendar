namespace minicalendar.Common.Calendars;

public static class Style
{
    public static string GetColourStyle(string style, int colourStyle)
    {
        return colourStyle switch
        {
            1 => $"{style}-green-400",
            2 => $"{style}-purple-400",
            3 => $"{style}-cyan-400",
            4 => $"{style}-indigo-400",
            5 => $"{style}-yellow-400 ",
            6 => $"{style}-red-400",
            _ => string.Empty
        };
    }

    private static string GetAltColourStyle(string style, int colourStyle)
    {
        return colourStyle switch
        {
            1 => $"{style}-green-500",
            2 => $"{style}-purple-500",
            3 => $"{style}-cyan-500",
            4 => $"{style}-indigo-500",
            5 => $"{style}-yellow-500 ",
            6 => $"{style}-red-500",
            _ => string.Empty
        };
    }

    private static string GetDeactivatedColourStyle(string style, int colourStyle)
    {
        return colourStyle switch
        {
            1 => $"{style}-green-200",
            2 => $"{style}-purple-200",
            3 => $"{style}-cyan-200",
            4 => $"{style}-indigo-200",
            5 => $"{style}-yellow-200 ",
            6 => $"{style}-red-200",
            _ => string.Empty
        };
    }

    public static string GetBackgroundStyle(int style, bool useAltStyle = false)
    {
        // Emoji
        if (style == 0)
        {
            return "text-gray-900";
        }
        return "text-white " + (useAltStyle ? GetAltColourStyle("bg", style) : 
            GetColourStyle("bg", style));
    }
    
    public static string GetBorderStyle(int style, bool isActivated = true)
    {
        return isActivated ? $"{GetColourStyle("border", style)}" : $"{GetDeactivatedColourStyle("border", style)}";
    }
}