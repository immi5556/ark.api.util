using System;
using System.Linq;

namespace ark.net.util
{
    public class ArkHelper
    {
        static Random r = new Random();
        public static int Random(int max)
        {
            return r.Next(max);
        }
        public static int Random(int min, int max)
        {
            return r.Next(min, max);
        }
        static Random random = new Random();
        public static string RandomString(int length, bool only_alphabets = false)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const string alph_chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (!only_alphabets)
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

            return new string(Enumerable.Repeat(alph_chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static DateTime GetRandomDate(int minYear = 1900, int maxYear = 2099)
        {
            var year = random.Next(minYear, maxYear);
            var month = random.Next(1, 12);
            var noOfDaysInMonth = DateTime.DaysInMonth(year, month);
            var day = random.Next(1, noOfDaysInMonth);

            return new DateTime(year, month, day);
        }
    }
}
