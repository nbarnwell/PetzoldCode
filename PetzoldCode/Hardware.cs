using System;

namespace PetzoldCode
{
    public static class Hardware
    {
        public static Func<bool> Relay(Func<bool> input, bool power = true)
        {
            return () => input() && power;
        }
    }
}