using System.ComponentModel.DataAnnotations;
using minicalendar.Common.Core;

namespace minicalendar.Common.Calendars.Activities;

public class Activity
{
    public static readonly Activity Empty = new();

    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "A title is required.")]
    [MaxLength(100, ErrorMessage = "The title must be less than 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [Range(0, 6, ErrorMessage = "A colour must be selected.")]
    public int ColourKind { get; set; } = 1;
    
    [Range(0, 2, ErrorMessage = "A style must be selected.")]
    public int StyleKind { get; set; } = 1;

    public List<DateOnly> Dates { get; set; } = [];

    public bool IsShown { get; set; }  = true;

    public string Emoji => Title.StartingEmoji();

    public void PopulateFrom(Activity src)
    {
        Title = src.Title;
        ColourKind = src.ColourKind;
        StyleKind = src.StyleKind;
        Dates = src.Dates.Select(x => x).Distinct().ToList();
    }

    public void AddDate(DateOnly date)
    {
        if (Dates.Contains(date))
        {
            return;
        }

        Dates.Add(date);
    }

    public void RemoveDate(DateOnly date)
    {
        Dates.Remove(date);
    }

    public string GetAbbreviatedTitle(int length = 2)
    {
        return string.IsNullOrEmpty(Emoji) ? string.Concat(Title.GetInitials().Take(length)) : Emoji;
    }
}