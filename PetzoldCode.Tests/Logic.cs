using System;

namespace PetzoldCode.Tests
{
    public static class Logic
    {
        public static Func<bool> Buffer(Func<bool> input, bool power = true)
        {
            return Hardware.Relay(input, power);
        }

        public static Func<bool> Inverter(Func<bool> input, bool power = true)
        {
            return Hardware.Relay(() => !input(), power);
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
        public static Func<bool> AndGate(Func<bool> input1, Func<bool> input2)
        {
            var relay1 = Hardware.Relay(input1);
            var relay2 = Hardware.Relay(input2, relay1());

            return relay2;
        }

        public static Func<bool> NandGate(Func<bool> input1, Func<bool> input2)
        {
            var andGate = AndGate(input1, input2);
            var inverter = Inverter(andGate);

            return inverter;
        }

        public static Func<bool> OrGate(Func<bool> input1, Func<bool> input2)
        {
            var relay1 = Hardware.Relay(input1);
            var relay2 = Hardware.Relay(input2);

            return () => relay1() || relay2();
        }

        public static Func<bool> NorGate(Func<bool> input1, Func<bool> input2)
        {
            var orGate = OrGate(input1, input2);
            var inverter = Inverter(orGate);

            return inverter;
        }
    }
}