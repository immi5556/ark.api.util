using System;

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
    }
}
