using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoWeb
{
    public static class Rand
    {
        static Random r = new Random();

        public static int Next(int min, int max)
        {
            return r.Next(min, max);
        }
    }
}