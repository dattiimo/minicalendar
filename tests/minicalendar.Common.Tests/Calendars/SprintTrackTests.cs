using minicalendar.Common.Calendars;

namespace minicalendar.Common.Tests.Calendars;

public class SprintTrackTests
{
    [Fact]
    public void IsAlternate_ShouldReturnFalse_WhenSameDay()
    {
        var track = new SprintTrack(new DateOnly(2024, 1, 1), 7);

        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 1)));
    }

    [Fact]
    public void IsAlternate_ShouldReturnFalse_WhenSamePeriodAfter()
    {
        var track = new SprintTrack(new DateOnly(2024, 1, 1), 7);

        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 2)));
        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 3)));
        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 4)));
        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 5)));
        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 6)));
        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 7)));
        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 8)));
    }

    [Fact]
    public void IsAlternate_ShouldReturnTrue_WhenDifferentPeriodAfter()
    {
        var track = new SprintTrack(new DateOnly(2024, 1, 1), 7);

        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 8)));
        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 9)));
        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 10)));
        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 11)));
        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 12)));
        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 13)));
        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 14)));
        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 15)));
    }

    [Fact]
    public void IsAlternate_ShouldReturnFalse_WhenSamePeriodBefore()
    {
        var track = new SprintTrack(new DateOnly(2024, 1, 15), 7);

        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 14)));
        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 13)));
        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 12)));
        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 11)));
        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 10)));
        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 9)));
        Assert.True(track.IsAlternate(new DateOnly(2024, 1, 8)));
        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 7)));
    }

    [Fact]
    public void IsAlternate_ShouldReturnTrue_WhenDifferentPeriodBefore()
    {
        var track = new SprintTrack(new DateOnly(2024, 1, 15), 7);

        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 7)));
        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 6)));
        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 5)));
        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 4)));
        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 3)));
        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 2)));
        Assert.False(track.IsAlternate(new DateOnly(2024, 1, 1)));
        Assert.True(track.IsAlternate(new DateOnly(2023, 12, 31)));
    }
}