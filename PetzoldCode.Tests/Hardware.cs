using System;

namespace PetzoldCode.Tests
{
    static internal class Hardware
    {
        public static Func<bool> Relay(Func<bool> input, bool power = true)
        {
            return () => input() && power;
        }
    }
}