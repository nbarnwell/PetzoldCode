using System;

namespace PetzoldCode.Tests
{
    public static class Logic
    {
        public static Func<bool> Buffer(bool input, bool power = true)
        {
            return Hardware.Relay(input, power);
        }

        public static Func<bool> Inverter(bool input, bool power = true)
        {
            return Hardware.Relay(!input, power);
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
            var relay1 = Hardware.Relay(input1);
            var relay2 = Hardware.Relay(input2, relay1());

            return relay2;
        }

        public static Func<bool> NandGate(bool input1, bool input2)
        {
            var andGate = AndGate(input1, input2);
            var inverter = Inverter(andGate());

            return inverter;
        }

        public static Func<bool> OrGate(bool input1, bool input2)
        {
            var relay1 = Hardware.Relay(input1);
            var relay2 = Hardware.Relay(input2);

            return () => relay1() || relay2();
        }

        public static Func<bool> NorGate(bool input1, bool input2)
        {
            var orGate = OrGate(input1, input2);
            var inverter = Inverter(orGate());

            return inverter;
        }
    }
}