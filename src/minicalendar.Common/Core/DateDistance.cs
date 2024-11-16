namespace minicalendar.Common.Core;

public class DateDistance
{
    public string TimeAway(DateOnly from, DateOnly to)
    {
        // April 27 is today
        // May 4 is tomorrow
        // November 13 is 5 days away
        // March 17 is 3 weeks and 2 days away
        // January 31 is yesterday
        // October 31 was 5 days ago
        // July 4 was 5 weeks and 1 day ago

        var txt = string.Empty;
        if (from == to)
        {
            //return $"{to.ToString("MMMM d")} is today";
            return txt;
        }

        // if (to > from)
        // {
            var diff = to.DayNumber - from.DayNumber;
            if (diff < 0)
            {
                diff *= -1;
            }
            var weeks = diff / 7;
            var days = diff % 7;
            //txt = $"{to.ToString("MMMM d")} is";
            
            if (weeks > 0)
            {
                txt += weeks + " week".Pluralise(weeks);
            }
            if (days > 0)
            {
                if (weeks > 0)
                {
                    txt += " and ";
                }
                txt += days + " day".Pluralise(days);
            }

            txt += " away";
        // }
        // else
        // {
        //     var diff = from.DayNumber - to.DayNumber;
        //     var weeks = diff / 7;
        //     var days = diff % 7;
        //     txt = $"{to.ToString("MMMM d")} was"; 
        //     if (weeks > 0)
        //     {
        //         txt += " " + weeks + " week".Pluralise(weeks);
        //     }
        //     if (days > 0)
        //     {
        //         if (weeks > 0)
        //         {
        //             txt += " and";
        //         }
        //         txt += " " + days + " day".Pluralise(days);
        //     }
        //
        //     txt += " ago";
        // }
        return txt;
    }

    public string TimeBetween(DateOnly from, DateOnly to)
    {
        // March 17 is 6 weeks and 6 days from May 4

        var txt = string.Empty;
        var diff = from.DayNumber - to.DayNumber;
        if (diff < 0)
        {
            diff *= -1;
        }
        
        var weeks = diff / 7;
        var days = diff % 7;

        if (weeks > 0)
        {
            txt += weeks + " week".Pluralise(weeks);
        }
        if (days > 0)
        {
            if (weeks > 0)
            {
                txt += " and ";
            }
            txt += days + " day".Pluralise(days);
        }

        txt += " apart";
        return txt;
    }
}