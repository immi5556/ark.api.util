using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ark.net.util
{
    public class DateUtil
    {
        public static Func<string> CurrentDateStamp = () => $"{DateTime.Now.Year}_{DateTime.Now.Month.ToString().PadLeft(2, '0')}_{DateTime.Now.Day.ToString().PadLeft(2, '0')}";
        public static Func<string> CurrentTimeStamp = () => $"{DateTime.Now.Hour.ToString().PadLeft(2, '0')}_{DateTime.Now.Minute.ToString().PadLeft(2, '0')}_{DateTime.Now.Second.ToString().PadLeft(2, '0')}";
        public static Func<string> CurrentTimeMsStamp = () => $"{DateTime.Now.Hour.ToString().PadLeft(2, '0')}_{DateTime.Now.Minute.ToString().PadLeft(2, '0')}_{DateTime.Now.Second.ToString().PadLeft(2, '0')}_{DateTime.Now.Millisecond.ToString().PadLeft(2, '0')}";
    }
}