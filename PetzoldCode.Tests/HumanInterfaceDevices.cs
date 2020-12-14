using System;

namespace PetzoldCode.Tests
{
    static internal class HumanInterfaceDevices
    {
        public static Func<bool> Lamp(bool input)
        {
            return () => input;
        }

        public static Func<bool> Switch(bool closed)
        {
            return () => closed;
        }
    }
}