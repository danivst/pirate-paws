using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class Utils
{
    public static long GetTimestamp()
    {
        return DateTime.Now.Ticks;
    }

    public static long TicksTillEndOfDay()
    {
        DateTime dateTime = DateTime.Today;
        DateTime endOfDay = dateTime.AddDays(1); // Correctly assign the result of AddDays
        return endOfDay.Ticks - GetTimestamp();
    }
    public static bool IsPreviousDay(long ticks)
    {
        DateTime givenDateTime = new DateTime(ticks);
        DateTime now = DateTime.Now;

        return (givenDateTime.Day != now.Day || givenDateTime.Month != now.Month) && now > givenDateTime;
    }
    public static string FormatNumber(float num)
    {
        if (num >= 1000000000)
        {
           return (num / (int)1000000000).ToString("0.##", CultureInfo.InvariantCulture) + "B";
        }
        else if (num >= 1000000)
        {
            return (num / (int)1000000).ToString("0.##", CultureInfo.InvariantCulture ) + "M";
        }
        else if (num >= 1000)
        {
            return (num / (int)1000).ToString("0.##", CultureInfo.InvariantCulture) + "K";
        }
        else
        {
            return num.ToString("0.##");
        }
    }
}

