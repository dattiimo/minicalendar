using System.Text.RegularExpressions;

namespace minicalendar.Common.Core;

public static class UriHelper
{
    private static readonly Regex WhitespaceRegx = new("\\s");

    public static string ToSeoUriPath(this string source)
    {
        if (string.IsNullOrEmpty(source))
        {
            return string.Empty;
        }

        var path = WhitespaceRegx.Replace(source, "-"); // replace whitespace with hyphen
        path = path.Replace("'", "", StringComparison.OrdinalIgnoreCase); // remove single quotes
        return Uri.EscapeDataString(path).ToLower();;
    }
}