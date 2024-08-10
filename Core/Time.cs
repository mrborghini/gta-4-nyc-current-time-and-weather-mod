using System;

namespace GtaIVTimeAndWeater.Core
{
    public class Time
    {
        public int CurrentHour { get; private set; }

        public int CurrentMinute { get; private set; }

        public void UpdateTime()
        {
            // Get the time zone for New York (Eastern Standard Time)
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            // Get the current time in UTC
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to New York time
            DateTime newYorkTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, easternZone);

            // Set them to the current time
            CurrentHour = newYorkTime.Hour;
            CurrentMinute = newYorkTime.Minute;
        }
    }
}