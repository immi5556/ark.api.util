using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ark.net.util
{
    public class DateUtil
    {
        public static Func<string> CurrentYearStamp = () => $"{DateTime.Now.Year}";
        public static Func<string> CurrentMonthStamp = () => $"{DateTime.Now.Year}_{DateTime.Now.Month.ToString().PadLeft(2, '0')}";
        public static Func<string> CurrentDateStamp = () => $"{DateTime.Now.Year}_{DateTime.Now.Month.ToString().PadLeft(2, '0')}_{DateTime.Now.Day.ToString().PadLeft(2, '0')}";
        public static Func<string> CurrentTimeOnlyStamp = () => $"{DateTime.Now.Hour.ToString().PadLeft(2, '0')}_{DateTime.Now.Minute.ToString().PadLeft(2, '0')}_{DateTime.Now.Second.ToString().PadLeft(2, '0')}";
        public static Func<string> CurrentTimeOnlyMsStamp = () => $"{DateTime.Now.Hour.ToString().PadLeft(2, '0')}_{DateTime.Now.Minute.ToString().PadLeft(2, '0')}_{DateTime.Now.Second.ToString().PadLeft(2, '0')}_{DateTime.Now.Millisecond.ToString().PadLeft(6, '0')}"; //millsecond stamp with 6 digits
        public static Func<string> CurrentTimeStamp = () => $"{CurrentDateStamp()}_{CurrentTimeOnlyMsStamp()}";
        public static Func<double> CurrentEpochTime = () =>  (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        public static Func<double?, DateTime?> EpochToDateTime = (epoch) => epoch.HasValue ? new DateTime(1970, 1, 1).AddSeconds(epoch.Value) : null;
    }
}