using minicalendar.Common.Core;

namespace minicalendar.Common.Tests.Core;

public class DateDistanceTests
{
    [Fact]
    public void Difference_ShouldBeEqual_WhenDatesAreTheSame()
    {
        var baseDate = new DateOnly(2024, 1, 1);

        var diff = DateOnlyExtensions.Difference(baseDate, new DateOnly(2024, 1, 1));

        Assert.Equal(DateOnlyComparative.Equal, diff.Comparative);
        Assert.Equal(0, diff.TotalDays);
        Assert.Equal(0, diff.Weeks);
        Assert.Equal(0, diff.Days);
    }

    [Fact]
    public void Difference_ShouldBeAfter_WhenDatesAreAfter()
    {
        var baseDate = new DateOnly(2024, 1, 1);

        var diff = DateOnlyExtensions.Difference(baseDate, new DateOnly(2024, 1, 2));
        Assert.Equal(DateOnlyComparative.After, diff.Comparative);
        Assert.Equal(1, diff.TotalDays);
        Assert.Equal(0, diff.Weeks);
        Assert.Equal(1, diff.Days);
        
        diff = DateOnlyExtensions.Difference(baseDate, new DateOnly(2024, 1, 7));
        Assert.Equal(DateOnlyComparative.After, diff.Comparative);
        Assert.Equal(6, diff.TotalDays);
        Assert.Equal(0, diff.Weeks);
        Assert.Equal(6, diff.Days);
        
        diff = DateOnlyExtensions.Difference(baseDate, new DateOnly(2024, 1, 8));
        Assert.Equal(DateOnlyComparative.After, diff.Comparative);
        Assert.Equal(7, diff.TotalDays);
        Assert.Equal(1, diff.Weeks);
        Assert.Equal(0, diff.Days);
        
        diff = DateOnlyExtensions.Difference(baseDate, new DateOnly(2024, 1, 9));
        Assert.Equal(DateOnlyComparative.After, diff.Comparative);
        Assert.Equal(8, diff.TotalDays);
        Assert.Equal(1, diff.Weeks);
        Assert.Equal(1, diff.Days);
    }

    [Fact]
    public void Difference_ShouldBeBefore_WhenDatesAreBefore()
    {
        var baseDate = new DateOnly(2024, 1, 15);

        var diff = DateOnlyExtensions.Difference(baseDate, new DateOnly(2024, 1, 14));
        Assert.Equal(DateOnlyComparative.Before, diff.Comparative);
        Assert.Equal(1, diff.TotalDays);
        Assert.Equal(0, diff.Weeks);
        Assert.Equal(1, diff.Days);

        diff = DateOnlyExtensions.Difference(baseDate, new DateOnly(2024, 1, 9));
        Assert.Equal(DateOnlyComparative.Before, diff.Comparative);
        Assert.Equal(6, diff.TotalDays);
        Assert.Equal(0, diff.Weeks);
        Assert.Equal(6, diff.Days);

        diff = DateOnlyExtensions.Difference(baseDate, new DateOnly(2024, 1, 8));
        Assert.Equal(DateOnlyComparative.Before, diff.Comparative);
        Assert.Equal(7, diff.TotalDays);
        Assert.Equal(1, diff.Weeks);
        Assert.Equal(0, diff.Days);

        diff = DateOnlyExtensions.Difference(baseDate, new DateOnly(2024, 1, 7));
        Assert.Equal(DateOnlyComparative.Before, diff.Comparative);
        Assert.Equal(8, diff.TotalDays);
        Assert.Equal(1, diff.Weeks);
        Assert.Equal(1, diff.Days);
    }

    [Fact]
    public void PercentageOf_ShouldBeZero_WhenFirstDay()
    {
        var from = new DateOnly(2024, 1, 1);
        var to = from.AddYears(1);
        var now = new DateOnly(2024, 1, 1);

        var percent = new DateDistance().PercentageOf(now, from, to);

        Assert.Equal(0, percent);
    }

    [Fact]
    public void PercentageOf_ShouldBe100_WhenLastDay()
    {
        var from = new DateOnly(2024, 1, 1);
        var to = from.AddYears(1);
        var now = new DateOnly(2024, 12, 31);

        var percent = new DateDistance().PercentageOf(now, from, to);

        Assert.Equal(100, percent);
    }

}