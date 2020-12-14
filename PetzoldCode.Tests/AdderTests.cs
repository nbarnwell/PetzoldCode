using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace PetzoldCode.Tests
{
    [TestFixture]
    public class AdderTests
    {
        [Test]
        [TestCase(false, false, ExpectedResult = new [] { false, false })]
        [TestCase(false, true, ExpectedResult = new[] { true, false })]
        [TestCase(true, false, ExpectedResult = new[] { true, false })]
        [TestCase(true, true, ExpectedResult = new[] { false, true })]
        public bool[] Half_adder_behaviour(bool input1, bool input2)
        {
            var halfAdder = Logic.HalfAdder(() => input1, () => input2);
            return new[] {halfAdder.SumOut(), halfAdder.CarryOut()};
        }

        [Test]
        [TestCase(false, false, false, ExpectedResult = new [] { false, false })]
        [TestCase(false, true, false, ExpectedResult = new[] { true, false })]
        [TestCase(true, false, false, ExpectedResult = new[] { true, false })]
        [TestCase(true, true, false, ExpectedResult = new[] { false, true })]
        [TestCase(false, false, true, ExpectedResult = new[] { true, false })]
        [TestCase(false, true, true, ExpectedResult = new[] { false, true })]
        [TestCase(true, false, true, ExpectedResult = new[] { false, true })]
        [TestCase(true, true, true, ExpectedResult = new[] { true, true })]
        public bool[] Full_adder_behaviour(bool input1, bool input2, bool carry)
        {
            var adder = Logic.FullAdder(() => input1, () => input2, () => carry);
            return new[] {adder.SumOut(), adder.CarryOut()};
        }

        [Test]
        [TestCase("00000001", "00000000", ExpectedResult = "0-00000001")]
        [TestCase("00000001", "00000001", ExpectedResult = "0-00000010")]
        [TestCase("00000010", "00000001", ExpectedResult = "0-00000011")]
        public string NBitAdder_behaviour(string input1, string input2)
        {
            var inputArray1 = input1.Reverse().Select(x => x != '0').ToArray();
            var inputArray2 = input2.Reverse().Select(x => x != '0').ToArray();

            var adder = Logic.NBitAdder(inputArray1, inputArray2, false);

            var result = adder.Reverse();
            var carry  = result.Take(1).Select(x => x() ? '1' : '0').Single();
            var value  = result.Skip(1).Select(x => x() ? '1' : '0').ToArray();

            var actual = carry + "-" + string.Join("", value);
            return actual;
        }
    }
}