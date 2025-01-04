using System.Text.RegularExpressions;

namespace minicalendar.Common.Core;

public static class StringExtensions
{
    private static readonly Regex StartsWithEmojiRegex = new(@"^[\p{So}\p{Cs}]");

    /// <summary>
    /// Returns true if the given string is not null or empty
    /// </summary>
    public static bool HasText(this string source)
    {
        return !string.IsNullOrEmpty(source);
    }

    /// <summary>
    /// Returns the plural of a given word in a string.
    /// 'day' becomes 'days' and 'activity' becomes 'activities'.  
    /// </summary>
    public static string Pluralise(this string source, int number)
    {
        // Only pluralise on multiples
        if (number <= 1)
        {
            return source;
        }

        // Quick and dirty exception list for known variations
        if ("activity".Equals(source, StringComparison.OrdinalIgnoreCase))
        {
            return "activities";
        }

        // If not known then just append an 's'
        return source + "s";
    }

    /// <summary>
    /// Returns the initial character of each word from a give string in upper case.
    /// 'Lorem ipsum dolor' becomes 'LID' 
    /// </summary>
    public static char[] GetInitials(this string value)
    {
        var initials = value
            .Split([' '], StringSplitOptions.RemoveEmptyEntries)
            .Where(x => x.Length >= 1 && char.IsLetter(x[0]))
            .ToArray();
        if (initials.Length == 0)
        {
            return [];
        }

        if (initials[0].Length == 2)
        {
            return initials[0].ToCharArray();
        }

        return initials.Select(x => char.ToUpper(x[0])).ToArray();
    }

    /// <summary>
    /// Returns true if the given string starts with an emoji.
    /// </summary>
    private static bool StartsWithEmoji(this string value)
    {
        return StartsWithEmojiRegex.IsMatch(value);
    }

    /// <summary>
    /// Returns the starting emoji in the given string if the string starts with an emoji followed by a space.
    /// Returns empty string if the given string doesn't start with an emoji.
    /// Rudimentary support for complex emojis (such as Family) by requiring a space after the emoji.
    /// </summary>
    public static string StartingEmoji(this string value)
    {
        return StartsWithEmoji(value) ? value[..value.IndexOf(' ')] : string.Empty; 
    }
}