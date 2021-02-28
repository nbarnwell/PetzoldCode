using System;
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
        [TestCase(0, ExpectedResult = "00000000")]
        [TestCase(1, ExpectedResult = "00000001")]
        [TestCase(2, ExpectedResult = "00000010")]
        [TestCase(8, ExpectedResult = "00001000")]
        [TestCase(128, ExpectedResult = "10000000")]
        [TestCase(255, ExpectedResult = "11111111")]
        public string Convert_int_to_binary(int value)
        {
            return ConvertToBinary(value);
        }

        private static string ConvertToBinary(int value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }

        [Test]
        public void Convert_int_to_boolarray_and_back()
        {
            Enumerable.Range(1, int.MaxValue)
                      .Select(x =>
                      {
                          var arr = 
                              Convert.ToString(x, 2)
                                     .PadLeft(8, '0')
                                     //.Reverse()
                                     .Select(x => x == '1')
                                     .ToArray();

                          var bytes = new byte[4];
                          new BitArray(arr).CopyTo(bytes, 0);

                          Assert.AreEqual(x, BitConverter.ToInt32(bytes, 0));

                          return 0;
                      });
        }

        [Test]
        [TestCase(1, 0, 1, false)]
        [TestCase(1, 1, 2, false)]
        [TestCase(2, 1, 3, false)]
        public void NBitAdder_behaviour(int input1, int input2, int expectedResult, bool expectedCarryResult)
        {
            var inputArray1 = Convert.ToString(input1, 2).PadLeft(8, '0').Reverse().Select(x => x == '1').ToArray();
            var inputArray2 = Convert.ToString(input2, 2).PadLeft(8, '0').Reverse().Select(x => x == '1').ToArray();

            var adder = Logic.NBitAdder(inputArray1, inputArray2, false);

            var result = adder.Reverse().Select(x => x());
            var carry  = result.Take(1).Single();
            var value  = result.Skip(1).ToArray();

            var bytes = new byte[4];
            new BitArray(value).CopyTo(bytes, 0);

            Assert.AreEqual(expectedResult, BitConverter.ToInt32(bytes, 0));
            Assert.AreEqual(expectedCarryResult, carry);
        }
    }
}