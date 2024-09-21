using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ark.net.util
{
    public class DateUtil
    {
        //YearStamp
        public static Func<string> CurrentYearStamp = () => $"{DateTime.Now.Year}";
        public static Func<DateTime?, string> YearStamp = (dt) => $"{(dt.HasValue ? dt.Value.Year : DateTime.Now.Year)}";

        //MonthStamp
        public static Func<string> CurrentMonthStamp = () => $"{DateTime.Now.Year}_{DateTime.Now.Month.ToString().PadLeft(2, '0')}";
        public static Func<DateTime?, string> MonthStamp = (dt) => $"{(dt.HasValue ? dt.Value.Year : DateTime.Now.Year)}_{(dt.HasValue ? dt.Value.Month : DateTime.Now.Month).ToString().PadLeft(2, '0')}";
        
        //DayStamp
        public static Func<string> CurrentDateStamp = () => $"{DateTime.Now.Year}_{DateTime.Now.Month.ToString().PadLeft(2, '0')}_{DateTime.Now.Day.ToString().PadLeft(2, '0')}";
        public static Func<DateTime?, string> DateStamp = (dt) => $"{(dt.HasValue ? dt.Value.Year : DateTime.Now.Year)}_{(dt.HasValue ? dt.Value.Month : DateTime.Now.Month).ToString().PadLeft(2, '0')}_{(dt.HasValue ? dt.Value.Day : DateTime.Now.Day).ToString().PadLeft(2, '0')}";

        public static Func<string> CurrentTimeOnlyStamp = () => $"{DateTime.Now.Hour.ToString().PadLeft(2, '0')}_{DateTime.Now.Minute.ToString().PadLeft(2, '0')}_{DateTime.Now.Second.ToString().PadLeft(2, '0')}";
        public static Func<string> CurrentTimeOnlyMsStamp = () => $"{DateTime.Now.Hour.ToString().PadLeft(2, '0')}_{DateTime.Now.Minute.ToString().PadLeft(2, '0')}_{DateTime.Now.Second.ToString().PadLeft(2, '0')}_{DateTime.Now.Millisecond.ToString().PadLeft(6, '0')}"; //millsecond stamp with 6 digits
        public static Func<string> CurrentTimeStamp = () => $"{CurrentDateStamp()}_{CurrentTimeOnlyMsStamp()}";
        public static Func<double> CurrentEpochTime = () =>  (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        public static Func<double?, DateTime?> EpochToDateTime = (epoch) => epoch.HasValue ? new DateTime(1970, 1, 1).AddSeconds(epoch.Value) : null;
        //TimeZoneInfo.GetSystemTimeZones(); ex: "India Standard Time", "US Mountain Standard Time", "Central Standard Time" etc
        public static Func<double?, string, DateTime?> EpochToZoneDateTime = (epoch, tz) =>
        {
            if (!epoch.HasValue) return null;
            DateTime utc = new DateTime(1970, 1, 1).AddSeconds(epoch.Value);
            return TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.FindSystemTimeZoneById(tz));
        };
        public static Func<double?, DateTime?> EpochToIndiaDateTime = (epoch) => EpochToZoneDateTime(epoch, "India Standard Time");
    }
}