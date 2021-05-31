using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ark.net.util
{
    public class DateUtil
    {
        public static Func<string> CurrentTimeStamp = () => $"{DateTime.Now.Year}_{DateTime.Now.Month.ToString().PadLeft(2, '0')}_{DateTime.Now.Day.ToString().PadLeft(2, '0')}";
    }
}