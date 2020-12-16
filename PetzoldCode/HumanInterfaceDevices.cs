using System;

namespace PetzoldCode
{
    public static class HumanInterfaceDevices
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