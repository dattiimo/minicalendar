using minicalendar.Common.Core;

namespace minicalendar.Common.Calendars;

public class SprintTrack(DateOnly start, int durationInDays) : ITrack
{
    public DateOnly Start { get; } = start;
    public int DurationInDays { get; } = durationInDays;

    /// <summary>
    /// Groups dates into two periods of the give length starting from `Start`.
    /// The period after the first is deemed an "alternate" and every other period after.
    /// the period before is deemed an "alternate" and every other before that.
    /// </summary>
    public bool IsAlternate(DateOnly date)
    {
        var diff = Start.Difference(date);
        return diff.Comparative switch
        {
            // The starting date should always be standard day and not alternate.
            DateOnlyComparative.Equal => false,
            // For days before the starting date, the alternate period starts the day before the starting date
            // as the starting date is not an alternate. 
            DateOnlyComparative.Before => ((diff.TotalDays - 1) / DurationInDays) % 2 == 0,
            // For all future dates, alternate should be every other period. 
            _ => (diff.TotalDays / DurationInDays) % 2 != 0
        };
    }
}