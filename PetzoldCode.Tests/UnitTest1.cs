using NUnit.Framework;

namespace PetzoldCode.Tests
{
    public class Tests
    {
        [Test]
        [TestCase(false, ExpectedResult = false)]
        [TestCase(true, ExpectedResult = true)]
        public bool Switch_behaviour(bool input)
        {
            return Logic.Switch(input)();
        }

        [Test]
        [TestCase(false, ExpectedResult = false)]
        [TestCase(true, ExpectedResult = true)]
        public bool Lamp_behaviour(bool input)
        {
            return Logic.Lamp(input)();
        }

        [Test]
        [TestCase(false, ExpectedResult = false)]
        [TestCase(true, ExpectedResult = true)]
        public bool Relay_behaviour(bool input)
        {
            return Logic.Relay(input)();
        }

        [Test]
        [TestCase(false, ExpectedResult = true)]
        [TestCase(true, ExpectedResult = false)]
        public bool InverterRelay_behaviour(bool input)
        {
            return Logic.Inverter(input)();
        }

        [Test]
        [TestCase(false, false, ExpectedResult = false)]
        [TestCase(false, true, ExpectedResult = false)]
        [TestCase(true, false, ExpectedResult = false)]
        [TestCase(true, true, ExpectedResult = true)]
        public bool AndGate_behaviour(bool input1, bool input2)
        {
            return Logic.AndGate(input1, input2)();
        }

        [Test]
        [TestCase(false, false, ExpectedResult = false)]
        [TestCase(false, true, ExpectedResult = true)]
        [TestCase(true, false, ExpectedResult = true)]
        [TestCase(true, true, ExpectedResult = true)]
        public bool OrGate_behaviour(bool input1, bool input2)
        {
            return Logic.OrGate(input1, input2)();
        }

        [Test]
        [TestCase(false, false, ExpectedResult = true)]
        [TestCase(false, true, ExpectedResult = false)]
        [TestCase(true, false, ExpectedResult = false)]
        [TestCase(true, true, ExpectedResult = false)]
        public bool NorGate_behaviour(bool input1, bool input2)
        {
            return Logic.NorGate(input1, input2)();
        }
    }
}