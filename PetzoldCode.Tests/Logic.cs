using System;

namespace PetzoldCode.Tests
{
    public static class Logic
    {
        public static Func<bool> Relay(bool input, bool power = true)
        {
            return () => input && power;
        }

        public static Func<bool> InverterRelay(bool input, bool power = true)
        {
            return () => !input && power;
        }

        /// <summary>
        ///           _________
        ///  Input1 --|        \
        ///           |         \__Output
        ///  Input2 --|         /
        ///           |________/
        ///
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public static Func<bool> AndGate(bool input1, bool input2)
        {
            var relay1 = Relay(input1);
            var relay2 = Relay(input2, relay1());

            return relay2;
        }

        public static Func<bool> OrGate(bool input1, bool input2)
        {
            var relay1 = Relay(input1);
            var relay2 = Relay(input2);

            return () => relay1() || relay2();
        }

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