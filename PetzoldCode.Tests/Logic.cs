using System;
using System.Collections.Generic;

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

        public static Func<bool> XorGate(Func<bool> input1, Func<bool> input2)
        {
            var orGate = OrGate(input1, input2);
            var nandGate = NandGate(input1, input2);
            var andGate = AndGate(orGate, nandGate);

            return andGate;
        }

        public static AdderOutput HalfAdder(Func<bool> input1, Func<bool> input2)
        {
            var sumOut = XorGate(input1, input2);
            var carryOut = AndGate(input1, input2);

            return new AdderOutput
            {
                SumOut = sumOut,
                CarryOut = carryOut
            };
        }

        public static AdderOutput FullAdder(Func<bool> input1, Func<bool> input2, Func<bool> carry)
        {
            var inputAdder = HalfAdder(input1, input2);
            var carryAdder = HalfAdder(carry, inputAdder.SumOut);
            var carryOut = OrGate(carryAdder.CarryOut, inputAdder.CarryOut);

            return new AdderOutput
            {
                SumOut = carryAdder.SumOut,
                CarryOut = carryOut
            };
        }

        public static IEnumerable<Func<bool>> NBitAdder(bool[] input1, bool[] input2, bool carryIn)
        {
            AdderOutput adder = null;
            for (int i = 0; i < input1.Length; i++)
            {
                var index = i;
                adder = FullAdder(() => input1[index], () => input2[index], adder == null ? () => carryIn : adder.CarryOut);
                yield return adder.SumOut;
            }

            yield return adder.CarryOut;
        }

        public class AdderOutput
        {
            public Func<bool> SumOut { get; set; }
            public Func<bool> CarryOut { get; set; }
        }
    }
}