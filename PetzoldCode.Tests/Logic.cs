using System;

namespace PetzoldCode.Tests
{
    public static class Logic
    {
        public static Func<bool> Relay(bool input)
        {
            return () => input;
        }

        public static Func<bool> AndGate(bool input1, bool input2)
        {
            return () => Relay(input1)() && Relay(input2)();
        }

        public static Func<bool> OrGate(bool input1, bool input2)
        {
            return () => Relay(input1)() || Relay(input2)();
        }
    }
}