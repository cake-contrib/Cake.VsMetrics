using System;

namespace Cake.VsMetrics
{
    public static class Check
    {
        public static T RequireNonNull<T>(T t, string name)
        {
            if (t == null)
            {
                throw new ArgumentException(name);
            }

            return t;
        }
    }
}
