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
        [TestCase(new[] {false, false, false, false, false, false, false, true}, new[] {false, false, false, false, false, false, false, false}, ExpectedResult = new[] {false, false, false, false, false, false, false, true, false})]
        [TestCase(new[] {false, false, false, false, false, false, false, true}, new[] {false, false, false, false, false, false, false, true}, ExpectedResult = new[] {false, false, false, false, false, false, true, false, false})]
        [TestCase(new[] {false, false, false, false, false, false, true, false}, new[] {false, false, false, false, false, false, false, true}, ExpectedResult = new[] {false, false, false, false, false, false, true, true, false})]
        public bool[] NBitAdder_behaviour(bool[] inputArray1, bool[] inputArray2)
        {
            var adder = Logic.NBitAdder(inputArray1, inputArray2, false);

            var actual = adder.Select(x => x()).ToArray();
            return actual;
        }
    }
}